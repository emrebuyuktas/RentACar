using Core.Persistence.Repositories;
using RentACar.Domain.Entities;

namespace RentACar.Application.Services.Repositories;

public interface IFuelRepository : IAsyncRepository<Fuel, Guid>, IRepository<Fuel, Guid>
{
}