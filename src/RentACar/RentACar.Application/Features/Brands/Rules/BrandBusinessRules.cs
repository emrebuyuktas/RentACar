using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using RentACar.Application.Features.Brands.Constants;
using RentACar.Application.Services.Repositories;

namespace RentACar.Application.Features.Brands.Rules;

public class BrandBusinessRules : BaseBusinessRules
{
    private readonly IBrandRepository _brandRepository;

    public BrandBusinessRules(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task BrandNameCannotBeDuplicatedWhenInserted(string name)
    {
        var brand = await _brandRepository.GetAsync(x => x.Name.ToLower() == name.ToLower(), enableTracking:false);

        if (brand != null)
        {
            throw new BusinessException(BrandsMessages.BrandNameExist);
        }
    }
}
