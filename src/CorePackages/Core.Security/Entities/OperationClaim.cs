using Core.Persistence.Repositories;

namespace Core.Security.Entities;

public class OperationClaim : Entity<int>
{
    public string Name { get; set; }
    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }

    public OperationClaim(string name)
    {
        Name = name;
    }
}
