// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using EnrichedContact = Defra.Trade.API.CertificatesStore.V1.ApiClient.Model.CustomerContact;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.Mappers;

public class ContactInfoProfile : Profile
{
    public ContactInfoProfile()
    {
        CreateMap<EnrichedContact, EnrichedContact>()
            .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
            .ReverseMap();
    }
}