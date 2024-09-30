// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Microsoft.Extensions.Logging;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.Extensions;

public static partial class ILoggerExtensions
{
    [LoggerMessage(EventId = 5, Level = LogLevel.Information, Message = "Messages ID : {MessageId} received on {FunctionName}. GC with ID : {GcId}")]
    public static partial void MessageReceived(this ILogger logger, string messageId, string functionName, string gcId);

    [LoggerMessage(EventId = 6, Level = LogLevel.Information, Message = "Processing enrichment for GC with ID : {GcId}")]
    public static partial void ProcessingEnrichment(this ILogger logger, string gcId);

    [LoggerMessage(EventId = 7, Level = LogLevel.Information, Message = "Successfully processed enrichment for GC with ID : {GcId}")]
    public static partial void ProcessingEnrichmentSuccess(this ILogger logger, string gcId);

    [LoggerMessage(EventId = 8, Level = LogLevel.Error, Message = "Processing failed for GC with ID : {GcId}. Retry count: {RetryCount}")]
    public static partial void ProcessingEnrichmentFailed(this ILogger logger, Exception exception, string gcId, int retryCount);

    [LoggerMessage(EventId = 9, Level = LogLevel.Information, Message = "Successfully processed Message with ID : {MessageId} received on {FunctionName}. GC with ID : {GcId}")]
    public static partial void ProcessMessageSuccess(this ILogger logger, string messageId, string functionName, string gcId);

    [LoggerMessage(EventId = 10, Level = LogLevel.Information, Message = "Sending enriched GC with ID : {GcId} to certificate store")]
    public static partial void SendingMessageToCertificateStore(this ILogger logger, string gcId);

    [LoggerMessage(EventId = 12, Level = LogLevel.Information, Message = "Successfully sent enriched GC with ID : {GcId} to certificate store")]
    public static partial void SendingMessageToCertificateStoreSuccess(this ILogger logger, string gcId);

    [LoggerMessage(EventId = 15, Level = LogLevel.Information, Message = "Requesting enrichment data from CRM Adapter for GC with ID : {GcID}")]
    public static partial void RequestingEnrichmentData(this ILogger logger, string gcId);

    [LoggerMessage(EventId = 16, Level = LogLevel.Information, Message = "Successfully enriched GC with ID : {GcID}")]
    public static partial void RequestingEnrichmentDataSuccess(this ILogger logger, string gcId);

    [LoggerMessage(EventId = 17, Level = LogLevel.Error, Message = "Enriched {NameOfData} data request failed for GC with ID : {GcID}. Contact ID : {ContactId}")]
    public static partial void RequestingEnrichmentDataContactFailure(this ILogger logger, Exception exception, string nameOfData, string gcId, string contactId);

    [LoggerMessage(EventId = 20, Level = LogLevel.Information, Message = "Sending notification for GC with ID : {GcId} to GC Notifier")]
    public static partial void SendingMessageToNotifier(this ILogger logger, string gcId);

    [LoggerMessage(EventId = 21, Level = LogLevel.Information, Message = "Sent notification for GC with ID : {GcId} to GC Notifier")]
    public static partial void SendingMessageToNotifierSuccess(this ILogger logger, string gcId);

    [LoggerMessage(EventId = 22, Level = LogLevel.Error, Message = "Failed to send notification for GC with ID : {GcId}. Retry count: {RetryCount}")]
    public static partial void SendingMessageToNotifierFailure(this ILogger logger, Exception exception, string gcId, int retryCount);
}