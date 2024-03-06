

using API.Services;
using Business.Interface;
using DataAccess.UnitOfWork;

namespace API.Extensions;
public static class AppServiceExtension
{
    public static void BindServices(this IServiceCollection services){
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAPIGetter, APIGetter>();
    }
}
