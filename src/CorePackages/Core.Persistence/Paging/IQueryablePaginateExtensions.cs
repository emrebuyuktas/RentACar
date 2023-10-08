using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.Paging;

public static class IQueryablePaginateExtensions
{
    public static async Task<Paginate<T>> ToPaginateAsync<T>(
        this IQueryable<T> source,
        int index,
        int size,
        CancellationToken cancellationToken = default)
    {
        int count = await source.CountAsync(cancellationToken).ConfigureAwait(false);

        List<T> items = await source
            .Skip(index * size)
            .Take(size)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var list= new Paginate<T>
        {
            Index = index,
            Size = size,
            Count = count,
            Items = items
        };

        return list;
    }

    public static Paginate<T> ToPaginate<T>(
    this IQueryable<T> source,
    int index,
    int size)
    {
        int count = source.Count();

        List<T> items = source
            .Skip(index * size)
            .Take(size)
            .ToList();

        var list = new Paginate<T>
        {
            Index = index,
            Size = size,
            Count = count,
            Items = items
        };

        return list;
    }
}