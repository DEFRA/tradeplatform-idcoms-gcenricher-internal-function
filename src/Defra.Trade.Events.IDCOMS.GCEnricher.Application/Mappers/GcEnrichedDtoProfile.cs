// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.API.CertificatesStore.V1.ApiClient.Model;
using Defra.Trade.Events.IDCOMS.GCEnricher.Application.Models;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.Mappers;

public class GcEnrichedDtoProfile : Profile
{
    public GcEnrichedDtoProfile()
    {
        CreateMap<GcEnrichedData, IdcomsGeneralCertificateEnrichment>();
    }
}