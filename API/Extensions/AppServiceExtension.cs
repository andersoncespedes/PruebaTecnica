using API.Services;
using AspNetCoreRateLimit;
using Business.Interface;
using DataAccess.UnitOfWork;
using Microsoft.Extensions.Caching.Memory;

namespace API.Extensions;
public static class AppServiceExtension
{
    // Agregando los diferentes servicios
    public static void BindServices(this IServiceCollection services)
    {
        // servicio de unidad de trabajo donde convergen los repositorios
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        // Servicio de obtencion y mapeo del api
        services.AddScoped<IAPIGetter, APIGetter>();
        // servicio de cache 
        services.AddSingleton<IMemoryCache, MemoryCache>();
    }
    public static void ConfigureJson(this IServiceCollection services){
        // Configuracion para evitar excepciones de bucles infinitos en el json de respuesta
        services.AddControllersWithViews()
        .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
    }
    public static void ConfigureRatelimiting(this IServiceCollection services)
    {
        // Configuracion del ratelimiting
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
