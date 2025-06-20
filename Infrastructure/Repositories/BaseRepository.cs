using Application.Repositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private AutoVerseContext _dataBase;

    public BaseRepository(AutoVerseContext dataBase)
    {
        _dataBase = dataBase;
    }

    public virtual async Task<T?> GetByIdAsync(string id, CancellationToken token = default)
    {
        return await _dataBase.Set<T>().FirstOrDefaultAsync(p => p.Id == id, token);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return _dataBase.Set<T>().AsNoTracking().AsEnumerable();
    }

    public async Task<T> AddAsync(T entity, CancellationToken token = default)
    {
        await _dataBase.Set<T>().AddAsync(entity, token);

        return entity;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken token = default)
    {
        _dataBase.Entry(entity).State = EntityState.Modified;

        return entity;
    }

    public async Task<T?> DeleteAsync(string id, CancellationToken token = default)
    {
        var entity = await GetByIdAsync(id, token);
        _dataBase.Set<T>().Remove(entity);
        
        return entity;
    }

    public async Task SaveChangesAsync(CancellationToken token = default)
    {
        await _dataBase.SaveChangesAsync(token);
    }
}