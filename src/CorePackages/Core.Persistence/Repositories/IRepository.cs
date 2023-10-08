using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories;

public interface IRepository<TEntity, TEntityId> : IQuery<TEntity> where TEntity : Entity<TEntityId>
{
    TEntity? GetAsync(
    Expression<Func<TEntity, bool>> predicate,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
    bool withDeleted = false,
    bool enableTracking = true,
    CancellationToken cancellationToken = default);

    Paginate<TEntity> GetListAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 20,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);


    Paginate<TEntity> GetListByDynamicAsync(
        DynamicQuery dynamic,
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 20,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);


    bool AnyAsync(
               Expression<Func<TEntity, bool>>? predicate = null,
                      bool withDeleted = false,
                             CancellationToken cancellationToken = default);

    TEntity AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    ICollection<TEntity> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

    TEntity UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    ICollection<TEntity> UpdateRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

    TEntity DeleteAsync(TEntity entity, bool permanent = false, CancellationToken cancellationToken = default);

    ICollection<TEntity> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false, CancellationToken cancellationToken = default);
}