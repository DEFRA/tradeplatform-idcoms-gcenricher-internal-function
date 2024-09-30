// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.API.CertificatesStore.V1.ApiClient.Model;
using Defra.Trade.Events.IDCOMS.GCEnricher.Application.Models;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.Mappers;

public class GcEnrichedDataProfile : Profile
{
    public GcEnrichedDataProfile()
    {
        CreateMap<GcEnrichedData, IdcomsGeneralCertificateEnrichment>()
            .ForMember(d => d.GcId, opt => opt.MapFrom(s => s.GcId))
            .ForMember(d => d.Applicant, opt => opt.MapFrom(s => s.Applicant))
            .ForMember(d => d.Organisations, opt => opt.MapFrom(s => s.Organisations))
            .ForMember(d => d.Establishments, opt => opt.MapFrom(s => s.Establishments))
            .ReverseMap();
    }
}