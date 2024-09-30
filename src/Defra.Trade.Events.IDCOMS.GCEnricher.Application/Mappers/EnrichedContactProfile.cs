// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.API.CertificatesStore.V1.ApiClient.Model;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.Mappers;

public class EnrichedContactProfile : Profile
{
    public EnrichedContactProfile()
    {
        CreateMap<Defra.Trade.CrmAdapter.Api.V1.ApiClient.Model.EnrichedContact, CustomerContact>()
            .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
            .ReverseMap();
    }
}