// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.UnitTests.Mappers;

public class AddressProfileTests : EnrichedGcProfileTests
{
    [Fact]
    public void AddressProfile_ConvertFrom_IsValid()
    {
        // Arrange
        var fixture = new Fixture();

        var source = fixture.Create<CrmAdapter.Api.V1.ApiClient.Model.EnrichedAddress>();

        // Act
        var result = Mapper.Map<CrmAdapter.Api.V1.ApiClient.Model.EnrichedAddress, API.CertificatesStore.V1.ApiClient.Model.Address>(source);

        // Assert
        result.AddressLine1.ShouldBe(source.AddressLine1);
        result.AddressLine2.ShouldBe(source.AddressLine2);
        result.AddressLine3.ShouldBe(source.AddressLine3);
        result.PostCode.ShouldBe(source.PostCode);
        result.Town.ShouldBe(source.Town);
    }
}
