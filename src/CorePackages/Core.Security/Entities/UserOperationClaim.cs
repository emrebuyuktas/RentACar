using Core.Persistence.Repositories;

namespace Core.Security.Entities;

public class UserOperationClaim : Entity<int>
{
    public int UserId { get; set; }
    public int OperationCalimId { get; set; }
    public virtual User User { get; set; }
    public virtual OperationClaim OperationClaim { get; set; }

    public UserOperationClaim(int userId, int operationCalimId)
    {
        UserId = userId;
        OperationCalimId = operationCalimId;
    }
}
