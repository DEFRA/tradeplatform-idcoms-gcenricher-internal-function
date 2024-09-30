// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.IDCOMS.GCEnricher.Application.Models;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.UnitTests.Models;

public class GcEnricherSettingsTests
{
    [Fact]
    public void Options_ShouldBe_AsExpected()
    {
        // Act
        var sut = new GcEnricherSettings
        {
            GcCreatedQueue = "mocked-queue-name"
        };

        // Assert
        sut.GcCreatedQueue.ShouldBe("mocked-queue-name");
    }
}
