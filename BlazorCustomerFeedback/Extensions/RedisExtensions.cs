using StackExchange.Redis;
using Microsoft.Extensions.DependencyInjection;
using BlazorCustomerFeedback.Services;

namespace BlazorCustomerFeedback.Extensions;

public static class RedisExtensions
{
    public static IServiceCollection AddRedisCache(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddSingleton<IConnectionMultiplexer>(sp =>
            ConnectionMultiplexer.Connect(connectionString));

        services.AddSingleton<ICacheService, RedisCacheService>();
        services.Decorate<IFeedbackService, CachedFeedbackService>();

        return services;
    }
}