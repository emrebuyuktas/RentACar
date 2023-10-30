using AutoMapper;
using Core.Application.Piplines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using RentACar.Application.Services.Repositories;
using RentACar.Domain.Entities;

namespace RentACar.Application.Features.Brands.Queries.GetList;

public class GetListBrandQuery : IRequest<GetListResponse<GetBrandListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public string CacheKey  => $"GetListBrandQuery:{PageRequest.PageIndex}:{PageRequest.PageSize}";
    public bool BypassCache { get; }
    public TimeSpan? SlidingExpiration { get; }
    public string? CacheGroupKey => "GetBrands";
}

public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, GetListResponse<GetBrandListItemDto>>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public GetListBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<GetListResponse<GetBrandListItemDto>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
    {
        Paginate<Brand> brands = await _brandRepository.GetListAsync(index:request.PageRequest.PageIndex,size:request.PageRequest.PageSize);

        return _mapper.Map<GetListResponse<GetBrandListItemDto>>(brands);

    }
}