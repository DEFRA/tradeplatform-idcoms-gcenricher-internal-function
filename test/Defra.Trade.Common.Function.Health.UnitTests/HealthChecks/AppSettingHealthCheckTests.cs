﻿// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Common.Function.Health.HealthChecks;
using Microsoft.Extensions.Configuration;

namespace Defra.Trade.Common.Function.Health.UnitTests.HealthChecks;

public class AppSettingHealthCheckTests
{
    private readonly HealthCheckContext context;
    private readonly Mock<IConfiguration> configuration = new();
    private readonly AppSettingHealthCheck sut;

    public AppSettingHealthCheckTests()
    {
        var healthCheck = new Mock<IHealthCheck>();
        var registration = new HealthCheckRegistration("Partner_AppSetting", healthCheck.Object, null, null);
        context = new() { Registration = registration };
        sut = new(configuration.Object);
    }

    [Fact]
    public async Task CheckHealthAsync_AppSettingExists_ReturnsHealthyResponse()
    {
        // Arrange
        configuration.Setup(c => c["Partner_AppSetting"]).Returns("PET");

        // Act
        var result = await sut.CheckHealthAsync(context);

        // Assert
        result.Status.ShouldBe(HealthStatus.Healthy);
        result.Description.ShouldBe("Successfully validated app setting Partner_AppSetting");
    }

    [Fact]
    public async Task CheckHealthAsync_AppSettingDoesNotExist_ReturnsHealthyResponse()
    {
        // Arrange
        configuration.Setup(c => c["Partner_AppSetting"]).Returns(string.Empty);

        // Act
        var result = await sut.CheckHealthAsync(context);

        // Assert
        result.Status.ShouldBe(HealthStatus.Unhealthy);
        result.Description.ShouldBe("Error occurred validating app setting Partner_AppSetting as it is null or empty");
    }
}
