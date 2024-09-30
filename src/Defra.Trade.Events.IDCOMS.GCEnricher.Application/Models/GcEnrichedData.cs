// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;
using Defra.Trade.API.CertificatesStore.V1.ApiClient.Model;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.Models;

public class GcEnrichedData
{
    /// <summary>
    /// GC id.
    /// </summary>
    public string GcId { get; set; }

    /// <summary>
    /// Applicant.
    /// </summary>
    public CustomerContact Applicant { get; set; }

    /// <summary>
    /// Enriched Organisations.
    /// </summary>
    public IList<Organisation> Organisations { get; set; } = [];

    /// <summary>
    /// Enriched Establishments.
    /// </summary>
    public IList<Establishment> Establishments { get; set; } = [];
}
