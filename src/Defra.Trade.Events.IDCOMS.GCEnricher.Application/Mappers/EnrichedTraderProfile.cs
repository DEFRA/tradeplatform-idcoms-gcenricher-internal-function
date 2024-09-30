// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.CrmAdapter.Api.V1.ApiClient.Model;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.Mappers;

public class EnrichedTraderProfile : Profile
{
    public EnrichedTraderProfile()
    {
        CreateMap<EnrichedTrader, Defra.Trade.API.CertificatesStore.V1.ApiClient.Model.Organisation>()
                   .ReverseMap();
    }
}