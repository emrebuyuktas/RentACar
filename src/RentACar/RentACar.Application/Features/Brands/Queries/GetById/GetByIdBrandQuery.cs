using AutoMapper;
using MediatR;
using RentACar.Application.Services.Repositories;

namespace RentACar.Application.Features.Brands.Queries.GetById;

public class GetByIdBrandQuery : IRequest<GetByIdBrandDto>
{
    public Guid Id { get; set; }
}

public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, GetByIdBrandDto>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public GetByIdBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<GetByIdBrandDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetAsync(predicate: x => x.Id == request.Id);

        return _mapper.Map<GetByIdBrandDto>(brand);
    }
}