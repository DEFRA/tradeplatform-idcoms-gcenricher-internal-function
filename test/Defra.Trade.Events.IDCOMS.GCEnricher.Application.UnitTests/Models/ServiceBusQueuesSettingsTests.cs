// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.IDCOMS.GCEnricher.Application.Models.Settings;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.UnitTests.Models;

public class ServiceBusQueuesSettingsTests
{
    [Fact]
    public void ServiceBusQueueSettings_ShouldBe_AsExpected()
    {
        // Act
        var sut = new ServiceBusQueuesSettings
        {
            QueueNameEhcoRemosEnrichment = "mocked-queue-name",
            QueueNameEhcoRemosCreate = "mocked-create-queue-name",
            QueueNameEhcoRemosNotification = "mocked-notification-queue-name"
        };

        // Assert
        sut.QueueNameEhcoRemosEnrichment.ShouldBe("mocked-queue-name");
        sut.QueueNameEhcoRemosCreate.ShouldBe("mocked-create-queue-name");
        sut.QueueNameEhcoRemosNotification.ShouldBe("mocked-notification-queue-name");
    }
}
