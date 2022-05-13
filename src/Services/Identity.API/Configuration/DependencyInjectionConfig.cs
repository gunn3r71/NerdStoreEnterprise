using KissLog;
using Microsoft.Extensions.DependencyInjection;

namespace NerdStoreEnterprise.Services.Identity.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IKLogger>(_ => Logger.Factory.Get());
        }
    }
}