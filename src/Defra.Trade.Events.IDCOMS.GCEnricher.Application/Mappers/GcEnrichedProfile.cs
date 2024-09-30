// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.Events.IDCOMS.GCEnricher.Application.Dtos.Inbound;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.Mappers;

public class GcEnrichedProfile : Profile
{
    public GcEnrichedProfile()
    {
        CreateMap<GcEnrichmentInbound, GcEnrichmentRequest>();
    }
}