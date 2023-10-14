using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using RentACar.Application.Features.Brands.Commands.Create;
using RentACar.Application.Features.Brands.Commands.Delete;
using RentACar.Application.Features.Brands.Commands.Update;
using RentACar.Application.Features.Brands.Queries.GetById;
using RentACar.Application.Features.Brands.Queries.GetList;
using RentACar.Domain.Entities;

namespace RentACar.Application.Features.Brands.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Brand, CreateBrandCommand>().ReverseMap();
        CreateMap<Brand, CreatedBrandResponse>().ReverseMap();
        CreateMap<Brand, GetBrandListItemDto>().ReverseMap();
        CreateMap<Paginate<Brand>,GetListResponse<GetBrandListItemDto>>().ReverseMap();
        CreateMap<Brand, GetByIdBrandDto>().ReverseMap();
        CreateMap<Brand, UpdateBrandResponse>().ReverseMap();
        CreateMap<Brand, DeleteBrandResponse>().ReverseMap();
    }
}