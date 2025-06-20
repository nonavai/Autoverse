using Domain.Enums;

namespace Application.Caching;

public interface IViewCounterService
{
    Task IncrementViewCountAsync(ViewType viewType, string? id = null);
    Task IncrementViewsBulkAsync(ViewType entityType, IEnumerable<string> ids);
}