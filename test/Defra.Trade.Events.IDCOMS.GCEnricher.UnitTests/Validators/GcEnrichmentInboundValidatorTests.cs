// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoFixture;
using Defra.Trade.Events.IDCOMS.GCEnricher.Application.Dtos.Inbound;
using Defra.Trade.Events.IDCOMS.GCEnricher.Validators;
using Shouldly;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.UnitTests.Validators;

public class GcEnrichmentInboundValidatorTests
{
    private readonly Fixture _fixture;
    private readonly GcEnrichmentInboundValidator _validator;

    public GcEnrichmentInboundValidatorTests()
    {
        _fixture = new Fixture();
        _validator = new GcEnrichmentInboundValidator();
    }

    [Fact]
    public void Validate_With_ValidRequest_ReturnsTrue()
    {
        var createProductionAppCredentials = _fixture.Build<GcEnrichmentInbound>()
            .Create();
        var result = _validator.Validate(createProductionAppCredentials);

        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validate_With_InValidRequest_ReturnsHaveValidationError()
    {
        var createProductionAppCredentials = new GcEnrichmentInbound();
        var result = _validator.Validate(createProductionAppCredentials);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.Exists(x => x.PropertyName == "GcId").ShouldBeTrue();
        result.Errors.Find(x => x.PropertyName == "GcId").ErrorMessage.ShouldBe("Gc Id cannot be empty");
    }
}
