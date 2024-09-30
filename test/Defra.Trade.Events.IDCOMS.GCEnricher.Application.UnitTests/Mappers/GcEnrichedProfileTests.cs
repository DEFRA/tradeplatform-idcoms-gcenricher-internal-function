// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.IDCOMS.GCEnricher.Application.Dtos.Inbound;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.UnitTests.Mappers;

public class GcEnrichedProfileTests : EnrichedGcProfileTests
{
    [Fact]
    public void AutoMapper_ConvertFrom_IsValid()
    {
        // Arrange
        var fixture = new Fixture();

        var source = fixture.Create<GcEnrichmentInbound>();

        // Act
        var result = Mapper.Map<GcEnrichmentInbound, GcEnrichmentRequest>(source);

        // Assert
        result.GcId.ShouldBe(source.GcId);
        result.Applicant.DefraCustomer.UserId.ShouldBe(source.Applicant.DefraCustomer.UserId);
        result.Consignee.DefraCustomer.OrgId.ShouldBe(source.Consignee.DefraCustomer.OrgId);
        result.Consignor.DefraCustomer.OrgId.ShouldBe(source.Consignor.DefraCustomer.OrgId);
        result.DispatchLocation.Idcoms.EstablishmentId.ShouldBe(source.DispatchLocation.Idcoms.EstablishmentId);
        result.DestinationLocation.Idcoms.EstablishmentId.ShouldBe(source.DestinationLocation.Idcoms.EstablishmentId);
    }
}
