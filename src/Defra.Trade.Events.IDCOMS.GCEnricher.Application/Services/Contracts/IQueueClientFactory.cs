// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Microsoft.Azure.ServiceBus;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.Services.Contracts;

public interface IQueueClientFactory
{
    IQueueClient CreateNotifierQueueClient();
}