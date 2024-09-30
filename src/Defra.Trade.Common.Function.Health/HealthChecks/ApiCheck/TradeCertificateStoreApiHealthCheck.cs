// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Diagnostics.CodeAnalysis;
using Defra.Trade.API.CertificatesStore.V1.ApiClient.Api;

namespace Defra.Trade.Common.Function.Health.HealthChecks.ApiCheck;

/// <summary>
/// Health check for Trade Api,
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Health check extensions cant be unit tested")]
public class TradeCertificateStoreApiHealthCheck : InternalApiHealthCheck
{
    private readonly string baseUrl;

    private readonly ServiceProvider serviceProvider;
    public TradeCertificateStoreApiHealthCheck(ServiceProvider serviceProvider, string baseUrl)
    {
        this.baseUrl = baseUrl;
        this.serviceProvider = serviceProvider;
    }

    public override async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        await Task.Delay(5, cancellationToken).ConfigureAwait(false);
        var result = await ExecuteCheckAsync(context, cancellationToken);
        return result;
    }

    protected override async Task<HealthCheckResult> CheckHealthInternalAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            string name = context.Registration.Name;
            var sp = serviceProvider.GetRequiredService<IMonitorApi>();
            var resultCheck = await sp.CheckHealthWithHttpInfoAsync("1", cancellationToken: cancellationToken);
            var healthResponse = JsonSerializer.Deserialize<HealthCheckResponse>(resultCheck.RawContent);
            return HealthCheckResult.Healthy(healthResponse.Status.Equals("Healthy", StringComparison.OrdinalIgnoreCase) ? "Healthy" : "UnHealthy", new Dictionary<string, object> { { "url", baseUrl }, { "name", name } });
        }
        catch (Exception ex)
        {
            var data = new Dictionary<string, object> { { "url", baseUrl } };
            return HealthCheckResult.Unhealthy($"Exception during check: {ex.GetType().FullName}", ex, data);
        }
    }
}