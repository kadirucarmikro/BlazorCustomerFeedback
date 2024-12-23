using StackExchange.Redis;
using System.Text.Json;

namespace BlazorCustomerFeedback.Services;

public class RedisCacheService : ICacheService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _cache;
    private readonly ILogger<RedisCacheService> _logger;

    public RedisCacheService(IConnectionMultiplexer redis, ILogger<RedisCacheService> logger)
    {
        _redis = redis;
        _cache = redis.GetDatabase();
        _logger = logger;
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> getData, TimeSpan? expiry = null)
    {
        var value = await GetAsync<T>(key);
        if (value != null) return value;

        value = await getData();
        await SetAsync(key, value, expiry);
        return value;
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var value = await _cache.StringGetAsync(key);
        if (!value.HasValue) return default;

        try
        {
            return JsonSerializer.Deserialize<T>(value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deserializing cached value for key: {Key}", key);
            return default;
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var serializedValue = JsonSerializer.Serialize(value);
        await _cache.StringSetAsync(key, serializedValue, expiry);
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.KeyDeleteAsync(key);
    }
}