namespace BlazorCustomerFeedback.Services;

public interface ICacheService
{
    Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> getData, TimeSpan? expiry = null);
    Task<T> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
    Task RemoveAsync(string key);
}