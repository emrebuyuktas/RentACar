using AutoMapper;
using RentACar.Application.Features.Brands.Commands.Create;
using RentACar.Domain.Entities;

namespace RentACar.Application.Features.Brands.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Brand, CreateBrandCommand>().ReverseMap();
        CreateMap<Brand, CreatedBrandResponse>().ReverseMap();
    }
}