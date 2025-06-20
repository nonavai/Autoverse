using Domain.Entities;

namespace Application.Repositories;

public interface IMarkRepository : IBaseRepository<Mark>
{
    public Task<IEnumerable<Mark>?> GetAllMarksAsync(CancellationToken token = default);
}