using AutoMapper;
using Core.Application.Piplines.Caching;
using Core.Application.Piplines.Logging;
using Core.Application.Piplines.Transaction;
using MediatR;
using RentACar.Application.Features.Brands.Rules;
using RentACar.Application.Services.Repositories;
using RentACar.Domain.Entities;

namespace RentACar.Application.Features.Brands.Commands.Create;

public class CreateBrandCommand : IRequest<CreatedBrandResponse>, ITransactiononalRequest, ICacheRemoverRequest, ILoggableRequest
{
    public string Name { get; set; }

    public string CacheKey => "";

    public bool BypassCache => false;

    public string? CacheGroupKey => "GetBrands";
}

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;
    private readonly BrandBusinessRules _brandBusinessRules;

    public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
        _brandBusinessRules = brandBusinessRules;
    }

    public async Task<CreatedBrandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        await _brandBusinessRules.BrandNameCannotBeDuplicatedWhenInserted(request.Name);

        var brand = _mapper.Map<Brand>(request);
        brand.Id = Guid.NewGuid();

        var result = await _brandRepository.AddAsync(brand);

        return _mapper.Map<CreatedBrandResponse>(result);
    }
}