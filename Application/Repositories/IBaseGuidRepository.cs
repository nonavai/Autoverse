namespace Application.Repositories;

public interface IBaseGuidRepository<T>
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity, CancellationToken token = default);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken token = default);
    Task<T> UpdateAsync(T entity, CancellationToken token = default);
    Task<T?> DeleteAsync(Guid id, CancellationToken token = default);
    Task SaveChangesAsync(CancellationToken token = default);
}