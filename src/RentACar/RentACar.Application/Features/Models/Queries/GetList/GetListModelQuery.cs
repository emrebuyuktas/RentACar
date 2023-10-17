using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentACar.Application.Services.Repositories;

namespace RentACar.Application.Features.Models.Queries.GetList;

public class GetListModelQuery : IRequest<GetListResponse<GetListModelListItemDto>>
{
    public PageRequest PageRequest { get; set; }
}

public class GetListModelQuerHandler : IRequestHandler<GetListModelQuery, GetListResponse<GetListModelListItemDto>>
{
    private readonly IModelRepository _modelRepository;
    private readonly IMapper _mapper;

    public GetListModelQuerHandler(IModelRepository modelRepository, IMapper mapper)
    {
        _modelRepository = modelRepository;
        _mapper = mapper;
    }

    public async Task<GetListResponse<GetListModelListItemDto>> Handle(GetListModelQuery request, CancellationToken cancellationToken)
    {
        var list = await _modelRepository.GetListAsync(index: request.PageRequest.PageIndex, size: request.PageRequest.PageSize, 
            include: m=>m.Include(x => x.Brand).Include(x => x.Fuel).Include(x => x.Transmission));
        
        var response = _mapper.Map<GetListResponse<GetListModelListItemDto>>(list);

        return response;

    }
}