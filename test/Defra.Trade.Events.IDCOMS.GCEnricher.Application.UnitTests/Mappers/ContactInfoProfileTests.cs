// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using EnrichedContact = Defra.Trade.API.CertificatesStore.V1.ApiClient.Model.CustomerContact;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.UnitTests.Mappers;

public class ContactInfoProfileTests : EnrichedGcProfileTests
{
    [Fact]
    public void DefraCustomerInfoDto_ConvertFrom_IsValid()
    {
        // Arrange
        var fixture = new Fixture();

        var source = fixture.Create<EnrichedContact>();

        // Act
        var result = Mapper.Map<EnrichedContact, EnrichedContact>(source);

        // Assert
        result.Name.ShouldBe(source.Name);
    }
}
