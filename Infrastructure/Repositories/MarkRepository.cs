using Application.Repositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MarkRepository :  BaseRepository<Mark>, IMarkRepository
{
    private AutoVerseContext _dataBase;

    public MarkRepository(AutoVerseContext dataBase) : base(dataBase)
    {
        _dataBase = dataBase;
    }
    
    public async Task<IEnumerable<Mark>?> GetAllMarksAsync(CancellationToken token = default)
    {
        return await _dataBase.Marks
            .AsNoTracking()
            .ToListAsync(cancellationToken: token);
    }
}