// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.V1.ApiClient.Model;
using Defra.Trade.Events.IDCOMS.GCEnricher.Application.Models;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.UnitTests.Mappers;

public class GcEnrichedDtoProfileTests : EnrichedGcProfileTests
{
    [Fact]
    public void AutoMapper_ConvertFrom_IsValid()
    {
        // Arrange
        var fixture = new Fixture();

        var source = fixture.Create<GcEnrichedData>();

        // Act
        var result = Mapper.Map<GcEnrichedData, IdcomsGeneralCertificateEnrichment>(source);

        // Assert
        result.GcId.ShouldBe(source.GcId);
    }
}
