namespace Infrastructure.Extensions;

public static class BaseQueryableExtensions
{
    public static IQueryable<TEntity> Paginate<TEntity>(
        this IQueryable<TEntity> query,
        int pageSize,
        int pageNumber
    )
    {
        if (pageSize <= 0)
        {
            return query;
        }

        return query
            .Skip(pageNumber * pageSize)
            .Take(pageSize);
    }
}