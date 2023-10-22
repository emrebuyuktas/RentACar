using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using RentACar.Application.Features.Models.Queries.GetList;
using RentACar.Application.Features.Models.Queries.GetListByDynamic;
using RentACar.Domain.Entities;

namespace RentACar.Application.Features.Models.profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Model, GetListModelListItemDto>()
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.FuelName, opt => opt.MapFrom(src => src.Fuel.Name))
            .ForMember(dest => dest.TransmissionName, opt => opt.MapFrom(src => src.Transmission.Name))
            .ReverseMap();

        CreateMap<Model, GetListByDynamicModelListItemDto>()
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.FuelName, opt => opt.MapFrom(src => src.Fuel.Name))
            .ForMember(dest => dest.TransmissionName, opt => opt.MapFrom(src => src.Transmission.Name))
            .ReverseMap();

        CreateMap<Paginate<Model>,GetListResponse<GetListModelListItemDto>>().ReverseMap();
        CreateMap<Paginate<Model>, GetListResponse<GetListByDynamicModelListItemDto>>().ReverseMap();
    }
}
