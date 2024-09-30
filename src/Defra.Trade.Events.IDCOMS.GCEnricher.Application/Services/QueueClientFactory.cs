// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Concurrent;
using Defra.Trade.Events.IDCOMS.GCEnricher.Application.Models.Settings;
using Defra.Trade.Events.IDCOMS.GCEnricher.Application.Services.Contracts;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.Services;

public class QueueClientFactory(IOptions<ServiceBusQueuesSettings> serviceBusQueuesSettings) : IQueueClientFactory
{
    private readonly IOptions<ServiceBusQueuesSettings> _serviceBusQueuesSettings = serviceBusQueuesSettings ?? throw new ArgumentNullException(nameof(serviceBusQueuesSettings));
    private readonly ConcurrentDictionary<string, IQueueClient> _queueClients = new();

    public IQueueClient CreateNotifierQueueClient()
    {
        return _queueClients.GetOrAdd(_serviceBusQueuesSettings.Value.QueueNameEhcoRemosNotification, (key) => new QueueClient(
            _serviceBusQueuesSettings.Value.ConnectionString,
            key,
            ReceiveMode.PeekLock,
            RetryPolicy.Default));
    }
}
