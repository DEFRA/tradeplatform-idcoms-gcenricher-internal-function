// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using Azure.Messaging.ServiceBus;
using Defra.Trade.Common.Functions.Interfaces;
using Defra.Trade.Common.Functions.Models;
using Defra.Trade.Events.IDCOMS.GCEnricher.Application.Dtos.Inbound;
using Defra.Trade.Events.IDCOMS.GCEnricher.Application.Models;
using Defra.Trade.Events.IDCOMS.GCEnricher.Application.Services;
using Defra.Trade.Events.IDCOMS.GCEnricher.Application.Services.Contracts;
using Defra.Trade.Events.IDCOMS.GCEnricher.Tests.Common;
using FakeItEasy;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using CertificateStoreClient = Defra.Trade.API.CertificatesStore.V1.ApiClient.Client;
using CrmAdapterClient = Defra.Trade.CrmAdapter.Api.V1.ApiClient.Client;
using Times = Moq.Times;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.UnitTests.Services;

public class SbMessageProcessorTests
{
    private readonly IFixture _fixture;
    private readonly TradeEventMessageHeader _messageHeader;
    private readonly Mock<ILogger<SbMessageProcessor>> _mockLogger;
    private readonly Mock<IGcEnrichmentMessageProcessor> _mockMessageProcessor;
    private readonly Mock<IMessageRetryContextAccessor> _mockRetryAccessor;
    private readonly Mock<IAsyncCollector<ServiceBusMessage>> _mockRetryQueue;
    private readonly Mock<ServiceBusReceivedMessage> _mockMessage;
    private readonly SbMessageProcessor _sut;

    public SbMessageProcessorTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _mockMessageProcessor = new Mock<IGcEnrichmentMessageProcessor>();
        _mockLogger = new Mock<ILogger<SbMessageProcessor>>();
        _mockLogger.Setup(x => x.IsEnabled(LogLevel.Information)).Returns(true);
        _mockLogger.Setup(x => x.IsEnabled(LogLevel.Error)).Returns(true);
        _mockRetryAccessor = new Mock<IMessageRetryContextAccessor>();
        _mockRetryQueue = new Mock<IAsyncCollector<ServiceBusMessage>>();
        _mockMessage = _fixture.Freeze<Mock<ServiceBusReceivedMessage>>();

        _sut = new SbMessageProcessor(_mockLogger.Object, _mockMessageProcessor.Object, _mockRetryAccessor.Object);
        _messageHeader = new TradeEventMessageHeader { MessageId = "messageId" };
    }

    [Fact]
    public void Ctors_EnsureNotNullAndCorrectExceptionParameterName()
    {
        var assertion = new GuardClauseAssertion(_fixture);
        assertion.Verify(typeof(SbMessageProcessor).GetConstructors());
    }

    [Fact]
    public async Task Process_BuildCustomMessageHeaderAsync_Should_Not_Be_Null()
    {
        // Act
        var result = await _sut.BuildCustomMessageHeaderAsync();

        // Assert
        result.ShouldNotBeNull();
    }

    [Theory, AutoData]
    public async Task Process_CustomerPublisherMessageProcessor_ValidateMessageLabelAsync(TradeEventMessageHeader messageHeader)
    {
        // Arrange
        messageHeader.Label = GcMessageConstants.BrokerLabel;
        // Act
        bool result = await _sut.ValidateMessageLabelAsync(messageHeader);

        // Assert
        result.ShouldBeTrue();
    }

    [Theory, AutoData]
    public async Task Process_CustomerPublisherMessageProcessor_ValidateMessageLabelAsync_Not_Relevant_Label(TradeEventMessageHeader messageHeader)
    {
        // Arrange
        messageHeader.Label = "invalid-label";
        // Act
        bool result = await _sut.ValidateMessageLabelAsync(messageHeader);

        // Assert
        result.ShouldBeFalse();
    }

    [Theory, AutoData]
    public async Task Process_GetSchemaAsync_Should_Not_Be_Null(TradeEventMessageHeader messageHeader)
    {
        // Act
        string result = await _sut.GetSchemaAsync(messageHeader);

        // Assert
        result.ShouldNotBeNull();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(500)]
    [InlineData(599)]
    public async Task ProcessMessage_WhenCrmAdapterApiException_ShouldThrowActualAndRetry(int errorCode)
    {
        // Arrange
        _mockRetryAccessor
            .Setup(x => x.Context.Queue)
            .Returns(_mockRetryQueue.Object);

        _mockRetryAccessor
            .Setup(x => x.Context.Message)
            .Returns(_mockMessage.Object);

        var mockedGcCommand = new GcEnrichmentRequest();
        var mockException = new CrmAdapterClient.ApiException(errorCode, "mocked error");

        _mockMessageProcessor
            .Setup(x => x.ProcessMessage(mockedGcCommand, _messageHeader))
            .Throws(mockException);

        // Act && Assert
        await Assert.ThrowsAsync<CrmAdapterClient.ApiException>(
            async () => await _sut.ProcessAsync(mockedGcCommand, _messageHeader));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(500)]
    [InlineData(599)]
    public async Task ProcessMessage_WhenCertificateStoreApiException_ShouldThrowActualAndRetry(int errorCode)
    {
        // Arrange
        _mockRetryAccessor
            .Setup(x => x.Context.Queue)
            .Returns(_mockRetryQueue.Object);

        _mockRetryAccessor
            .Setup(x => x.Context.Message)
            .Returns(_mockMessage.Object);

        var mockedGcCommand = new GcEnrichmentRequest();
        var mockException = new CertificateStoreClient.ApiException(errorCode, "mocked error");

        _mockMessageProcessor
            .Setup(x => x.ProcessMessage(mockedGcCommand, _messageHeader))
            .Throws(mockException);

        // Act && Assert
        await Assert.ThrowsAsync<CertificateStoreClient.ApiException>(
            async () => await _sut.ProcessAsync(mockedGcCommand, _messageHeader));
    }

    [Fact]
    public async Task ProcessMessage_WithServiceBusException_ThrowsAndDoesNotRetry()
    {
        // Arrange
        _mockRetryAccessor
            .Setup(x => x.Context.Queue)
            .Returns(_mockRetryQueue.Object);

        _mockRetryAccessor
            .Setup(x => x.Context.Message)
            .Returns(_mockMessage.Object);

        var mockedGcCommand = _fixture.Create<GcEnrichmentRequest>();
        var mockException = new ServiceBusCommunicationException("mocked error");

        _mockMessageProcessor
            .Setup(x => x.ProcessMessage(mockedGcCommand, _messageHeader))
            .Throws(mockException);

        // Act
        await _sut.ProcessAsync(mockedGcCommand, _messageHeader);

        // Assert
        _mockLogger.VerifyLogged(
            $"Failed to send notification for GC with ID : {mockedGcCommand.GcId}. Retry count: {0}",
            LogLevel.Error);
    }

    [Fact]
    public async Task ProcessMessage_WhenValidJson_ShouldParse()
    {
        // Arrange
        int expectedRetryAttempt = 0;
        var mockedGcCommand = new GcEnrichmentRequest();
        _mockMessageProcessor.Setup(x => x.ProcessMessage(mockedGcCommand, _messageHeader))
            .Returns(Task.CompletedTask);
        var enqueueTime = DateTimeOffset.UtcNow;
        var expectedRequeueTime = enqueueTime.AddSeconds(30);
        var retryQueue = A.Fake<IAsyncCollector<ServiceBusMessage>>(p => p.Strict());

        var requeueMessage = A.CallTo(() => retryQueue.AddAsync(A<ServiceBusMessage>.That.Matches(m =>
            Equals(GetValueOrDefault(m.ApplicationProperties, "RetryCount"), expectedRetryAttempt)
            && m.ScheduledEnqueueTime == expectedRequeueTime), default));
        requeueMessage.Returns(Task.CompletedTask);

        // Act
        await _sut.ProcessAsync(mockedGcCommand, _messageHeader);

        // Assert
        _mockLogger.VerifyLogged(
            $"Successfully processed enrichment for GC with ID : {mockedGcCommand.GcId}",
            LogLevel.Information);

        _mockMessageProcessor.Verify(x => x.ProcessMessage(mockedGcCommand, _messageHeader), Times.Once());
        requeueMessage.MustNotHaveHappened();
    }

    private static TValue GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dict, TKey key)
        => dict.TryGetValue(key, out var value) ? value : default;
}
