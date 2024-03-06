

using API.Services;
using AspNetCoreRateLimit;
using Business.Interface;
using DataAccess.UnitOfWork;

namespace API.Extensions;
public static class AppServiceExtension
{
    public static void BindServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAPIGetter, APIGetter>();
    }
    public static void ConfigureRatelimiting(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();
        services.Configure<IpRateLimitOptions>(options =>
        {
            options.EnableEndpointRateLimiting = true;
            options.StackBlockedRequests = false;
            options.HttpStatusCode = 429;
            options.RealIpHeader = "X-Real-IP";
            options.GeneralRules = new List<RateLimitRule>
        {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Period = "100s",
                    Limit = 15
                }
            };
        });
    }
}
