// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Diagnostics.CodeAnalysis;
using Defra.Trade.CrmAdapter.Api.V1.ApiClient.Api;

namespace Defra.Trade.Common.Function.Health.HealthChecks.ApiCheck;

/// <summary>
/// Health check for Trade Api,
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Health check extensions cant be unit tested")]
public class CrmAdapterApiHealthCheck : InternalApiHealthCheck
{
    private readonly string baseUrl;
    private readonly string sampleUserId;

    private readonly ServiceProvider serviceProvider;
    public CrmAdapterApiHealthCheck(ServiceProvider serviceProvider, string baseUrl, string sampleUserId)
    {
        this.baseUrl = baseUrl;
        this.serviceProvider = serviceProvider;
        this.sampleUserId = sampleUserId;
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
            var gcId = new Guid(sampleUserId);
            string name = context.Registration.Name;
            var enrichmentApi = serviceProvider.GetRequiredService<IEnrichmentApi>();
            var resultCheck = await enrichmentApi.EnrichContactDetailsAsync(gcId, cancellationToken: cancellationToken);
            return HealthCheckResult.Healthy(resultCheck.Name is not null ? "Healthy" : "UnHealthy", new Dictionary<string, object> { { "url", baseUrl }, { "name", name } });
        }
        catch (Exception ex)
        {
            var data = new Dictionary<string, object> { { "url", baseUrl } };
            return HealthCheckResult.Unhealthy($"Exception during check: {ex.GetType().FullName}", ex, data);
        }
    }
}