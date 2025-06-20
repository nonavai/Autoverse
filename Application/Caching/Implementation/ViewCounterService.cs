using Domain.Enums;
using StackExchange.Redis;

namespace Application.Caching.Implementation;

public class ViewCounterService : IViewCounterService
{
    private readonly IDatabase _redis;

    public ViewCounterService(IConnectionMultiplexer redis)
    {
        _redis = redis.GetDatabase();
    }

    public async Task IncrementViewCountAsync(ViewType viewType, string? id = null)
    {
        var redisKey = GetRedisKey(viewType, id);
        await _redis.StringIncrementAsync(redisKey);
    }
    
    public async Task IncrementViewsBulkAsync(ViewType entityType, IEnumerable<string> ids)
    {
        var batch = _redis.CreateBatch();
        var tasks = new List<Task>();

        foreach (var id in ids.Distinct())
        {
            if (string.IsNullOrWhiteSpace(id))
                continue;

            var key = GetRedisKey(entityType, id);
            tasks.Add(batch.StringIncrementAsync(key));
        }

        batch.Execute();
        await Task.WhenAll(tasks);
    }

    private static string GetRedisKey(ViewType viewType, string? id)
    {
        return viewType switch
        {
            ViewType.Mark => $"views:mark:{id}",
            ViewType.Model => $"views:model:{id}",
            ViewType.Generation => $"views:generation:{id}",
            ViewType.Configuration => $"views:configuration:{id}",
            ViewType.Modification => $"views:modification:{id}",
            ViewType.Random => $"views:random",
            ViewType.Weekly => $"views:weekly",
            _ => throw new ArgumentException("Invalid entity type", nameof(viewType))
        };
    }
}