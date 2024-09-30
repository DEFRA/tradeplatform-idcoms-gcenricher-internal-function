// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using EnrichedCountry = Defra.Trade.API.CertificatesStore.V1.ApiClient.Model.Country;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.Mappers;

public class EnrichedCountryProfile : Profile
{
    public EnrichedCountryProfile()
    {
        CreateMap<Defra.Trade.CrmAdapter.Api.V1.ApiClient.Model.EnrichedCountry, EnrichedCountry>()
            .ReverseMap();
    }
}