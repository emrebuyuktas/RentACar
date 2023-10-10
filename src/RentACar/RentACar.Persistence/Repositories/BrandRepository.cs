using Core.Persistence.Repositories;
using RentACar.Application.Services.Repositories;
using RentACar.Domain.Entities;
using RentACar.Persistence.Context;

namespace RentACar.Persistence.Repositories;

public class BrandRepository : EfRepositoryBase<Brand, Guid,BaseDbContext>, IBrandRepository
{
    public BrandRepository(BaseDbContext dbContext) : base(dbContext)
    {
    }
}