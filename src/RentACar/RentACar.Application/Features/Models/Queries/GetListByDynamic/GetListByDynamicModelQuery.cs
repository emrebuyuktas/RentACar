using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentACar.Application.Features.Models.Queries.GetList;
using RentACar.Application.Services.Repositories;

namespace RentACar.Application.Features.Models.Queries.GetListByDynamic;

public class GetListByDynamicModelQuery : IRequest<GetListResponse<GetListByDynamicModelListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }
}

public class GetListByDynamicModelQueryHandler : IRequestHandler<GetListByDynamicModelQuery, GetListResponse<GetListByDynamicModelListItemDto>>
{
    private readonly IModelRepository _modelRepository;
    private readonly IMapper _mapper;

    public GetListByDynamicModelQueryHandler(IModelRepository modelRepository, IMapper mapper)
    {
        _modelRepository = modelRepository;
        _mapper = mapper;
    }

    public async Task<GetListResponse<GetListByDynamicModelListItemDto>> Handle(GetListByDynamicModelQuery request, CancellationToken cancellationToken)
    {
        var list = await _modelRepository.GetListByDynamicAsync(dynamic: request.DynamicQuery, index: request.PageRequest.PageIndex, size: request.PageRequest.PageSize,
    include: m => m.Include(x => x.Brand).Include(x => x.Fuel).Include(x => x.Transmission));

        var response = _mapper.Map<GetListResponse<GetListByDynamicModelListItemDto>>(list);

        return response;
    }
}