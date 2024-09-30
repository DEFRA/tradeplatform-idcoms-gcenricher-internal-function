// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.V1.ApiClient.Model;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.Dtos.Inbound;

/// <summary>
/// GC Enrichment data model.
/// </summary>
public class GcEnrichmentInbound
{
    /// <summary>
    /// General certificate id.
    /// </summary>
    public string GcId { get; set; }

    /// <summary>
    /// Applicant ids to enrich applicant info.
    /// </summary>
    public Applicant Applicant { get; set; }

    /// <summary>
    /// Consignor ids to enrich applicant info.
    /// </summary>
    public Consignor Consignor { get; set; }

    /// <summary>
    /// Consignee ids to enrich applicant info.
    /// </summary>
    public Consignee Consignee { get; set; }

    /// <summary>
    /// DispatchLocation / establishment ids to enrich applicant info.
    /// </summary>
    public LocationInfo DispatchLocation { get; set; }

    /// <summary>
    /// DestinationLocation / establishment ids to enrich applicant info.
    /// </summary>
    public LocationInfo DestinationLocation { get; set; }
}