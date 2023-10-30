using AutoMapper;
using Core.Application.Piplines.Caching;
using MediatR;
using RentACar.Application.Services.Repositories;

namespace RentACar.Application.Features.Brands.Commands.Update;

public class UpdateBrandCommand : IRequest<UpdateBrandResponse>, ICacheRemoverRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string CacheKey => "";

    public bool BypassCache => false;

    public string? CacheGroupKey => "GetBrands";
}

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, UpdateBrandResponse>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<UpdateBrandResponse> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetAsync(predicate: x => x.Id == request.Id);

        brand = _mapper.Map(request, brand);

        await _brandRepository.UpdateAsync(brand);

        return _mapper.Map<UpdateBrandResponse>(brand);
    }
}