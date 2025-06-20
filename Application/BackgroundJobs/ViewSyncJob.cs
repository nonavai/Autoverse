using Application.Repositories.VIew;
using Domain.Entities.CustomEntities;
using Domain.Enums;
using Hangfire;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

public class ViewSyncJob
{
    private readonly IConnectionMultiplexer _redis;
    private readonly ILogger<ViewSyncJob> _logger;
    private readonly IMarkViewRepository _markViewRepository;
    private readonly IModelViewRepository _modelViewRepository;
    private readonly IGenerationViewRepository _generationViewRepository;
    private readonly ICarConfigurationViewRepository _configurationViewRepository;
    private readonly IModificationViewRepository _modificationViewRepository;

    public ViewSyncJob(
        IConnectionMultiplexer redis,
        ILogger<ViewSyncJob> logger,
        IMarkViewRepository markViewRepository,
        IModelViewRepository modelViewRepository,
        IGenerationViewRepository generationViewRepository,
        ICarConfigurationViewRepository configurationViewRepository,
        IModificationViewRepository modificationViewRepository)
    {
        _redis = redis;
        _logger = logger;
        _markViewRepository = markViewRepository;
        _modelViewRepository = modelViewRepository;
        _generationViewRepository = generationViewRepository;
        _configurationViewRepository = configurationViewRepository;
        _modificationViewRepository = modificationViewRepository;
    }

    [AutomaticRetry(Attempts = 3)]
    public async Task SyncAllViewsAsync()
    {
        var processDate = DateTimeOffset.UtcNow.Date;
        
        foreach (ViewType entityType in Enum.GetValues(typeof(ViewType)))
        {
            try
            {
                await ProcessEntityTypeAsync(entityType, processDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing {entityType} views");
                throw;
            }
        }
    }

    private async Task ProcessEntityTypeAsync(ViewType entityType, DateTimeOffset processDate)
    {
        var db = _redis.GetDatabase();
        var pattern = GetKeyPattern(entityType);
        var server = _redis.GetServer(_redis.GetEndPoints().First());

        await foreach (var keys in GetKeysInBatchesAsync(server, pattern, 1000))
        {
            await ProcessKeysBatchAsync(db, entityType, keys, processDate);
        }
    }

    private async IAsyncEnumerable<RedisKey[]> GetKeysInBatchesAsync(
        IServer server, 
        string pattern, 
        int batchSize)
    {
        var keys = new List<RedisKey>();
        await foreach (var key in server.KeysAsync(pattern: pattern))
        {
            keys.Add(key);
            if (keys.Count >= batchSize)
            {
                yield return keys.ToArray();
                keys.Clear();
            }
        }

        if (keys.Count > 0)
        {
            yield return keys.ToArray();
        }
    }

    private async Task ProcessKeysBatchAsync(
        IDatabase db, 
        ViewType entityType, 
        RedisKey[] keys,
        DateTimeOffset processDate)
    {
        var viewCounts = new Dictionary<string, long>();
        var results = await db.StringGetAsync(keys);

        for (int i = 0; i < keys.Length; i++)
        {
            if (!results[i].HasValue || !long.TryParse(results[i], out var count))
                continue;

            var id = keys[i].ToString().Split(':')[2];
            viewCounts[id] = count;
        }

        if (!viewCounts.Any())
            return;

        await CreateViewRecordsAsync(entityType, viewCounts, processDate);
        await db.KeyDeleteAsync(keys);
    }

    private async Task CreateViewRecordsAsync(
        ViewType entityType,
        Dictionary<string, long> viewCounts,
        DateTimeOffset date)
    {
        switch (entityType)
        {
            case ViewType.Mark:
                await CreateMarkViewsAsync(viewCounts, date);
                break;
            case ViewType.Model:
                await CreateModelViewsAsync(viewCounts, date);
                break;
            case ViewType.Generation:
                await CreateGenerationViewsAsync(viewCounts, date);
                break;
            case ViewType.Configuration:
                await CreateConfigurationViewsAsync(viewCounts, date);
                break;
            case ViewType.Modification:
                await CreateModificationViewsAsync(viewCounts, date);
                break;
            default:
                throw new ArgumentException("Invalid entity type");
        }
    }

    private async Task CreateMarkViewsAsync(Dictionary<string, long> viewCounts, DateTimeOffset date)
    {
        var views = viewCounts.Select(x => new MarkView
        {
            Id = Guid.NewGuid(),
            MarkId = x.Key,
            Views = (int)x.Value,
            Date = date
        });

        await _markViewRepository.AddRangeAsync(views);
        await _markViewRepository.SaveChangesAsync();
    }

    private async Task CreateModelViewsAsync(Dictionary<string, long> viewCounts, DateTimeOffset date)
    {
        var views = viewCounts.Select(x => new ModelView
        {
            Id = Guid.NewGuid(),
            ModelId = x.Key,
            Views = (int)x.Value,
            Date = date
        });

        await _modelViewRepository.AddRangeAsync(views);
        await _modelViewRepository.SaveChangesAsync();
    }

    private async Task CreateGenerationViewsAsync(Dictionary<string, long> viewCounts, DateTimeOffset date)
    {
        var views = viewCounts.Select(x => new GenerationView
        {
            Id = Guid.NewGuid(),
            GenerationId = x.Key,
            Views = (int)x.Value,
            Date = date
        });

        await _generationViewRepository.AddRangeAsync(views);
        await _generationViewRepository.SaveChangesAsync();
    }

    private async Task CreateConfigurationViewsAsync(Dictionary<string, long> viewCounts, DateTimeOffset date)
    {
        var views = viewCounts.Select(x => new CarConfigurationView
        {
            Id = Guid.NewGuid(),
            CarConfigurationId = x.Key,
            Views = (int)x.Value,
            Date = date
        });

        await _configurationViewRepository.AddRangeAsync(views);
        await _configurationViewRepository.SaveChangesAsync();
    }

    private async Task CreateModificationViewsAsync(Dictionary<string, long> viewCounts, DateTimeOffset date)
    {
        var views = viewCounts.Select(x => new ModificationView
        {
            Id = Guid.NewGuid(),
            ModificationId = x.Key,
            Views = (int)x.Value,
            Date = date
        });

        await _modificationViewRepository.AddRangeAsync(views);
        await _modificationViewRepository.SaveChangesAsync();
    }

    private static string GetKeyPattern(ViewType entityType)
    {
        return $"views:{entityType.ToString().ToLower()}:*";
    }
}