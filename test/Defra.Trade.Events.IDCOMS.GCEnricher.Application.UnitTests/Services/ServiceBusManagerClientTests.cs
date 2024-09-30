// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.IDCOMS.GCEnricher.Application.Services;
using Defra.Trade.Events.IDCOMS.GCEnricher.Application.Services.Contracts;
using Microsoft.Azure.ServiceBus;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.UnitTests.Services;

public class ServiceBusManagerClientTests
{
    private readonly Mock<IQueueClientFactory> _queueClientFactory;
    private readonly IFixture _fixture;
    private readonly ServiceBusManagerClient _sut;

    public ServiceBusManagerClientTests()
    {
        _fixture = new Fixture();
        _queueClientFactory = new Mock<IQueueClientFactory>();
        _sut = new ServiceBusManagerClient(_queueClientFactory.Object);
    }

    [Fact]
    public async Task ServiceBusManagerClient_Should_SendMessage()
    {
        // Arrange
        var queueClient = new QueueClient(
            new ServiceBusConnectionStringBuilder(
                "https://testsbserver.azconfig.io",
                "TestEntityPath",
                "TestId",
                "TestSecret"));
        var message = _fixture.Create<Message>();

        _queueClientFactory.Setup(x =>
                x.CreateNotifierQueueClient())
            .Returns(queueClient);

        _queueClientFactory.Setup(x =>
                x.CreateNotifierQueueClient().SendAsync(message))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        await _sut.SendMessageAsync(message);

        // Assert
        _queueClientFactory.Verify(x =>
            x.CreateNotifierQueueClient().SendAsync(message), Times.Once());
    }
}
