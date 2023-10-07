using Core.Persistence.Repositories;

namespace RentACar.Domain.Entities;

public class Brand : Entity<Guid>
{
    public string Name { get; set; }
}
