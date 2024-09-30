// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.API.CertificatesStore.V1.ApiClient.Model;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.Mappers;

public class EnrichedApplicantProfile : Profile
{
    public EnrichedApplicantProfile()
    {
        CreateMap<Applicant, Applicant>()
            .ForMember(d => d.DefraCustomer, opt => opt.MapFrom(s => s.DefraCustomer))
            .ReverseMap();
    }
}