// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Address = Defra.Trade.API.CertificatesStore.V1.ApiClient.Model.Address;

namespace Defra.Trade.Events.IDCOMS.GCEnricher.Application.Mappers;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Defra.Trade.CrmAdapter.Api.V1.ApiClient.Model.EnrichedAddress, Address>()
             .ReverseMap();
    }
}