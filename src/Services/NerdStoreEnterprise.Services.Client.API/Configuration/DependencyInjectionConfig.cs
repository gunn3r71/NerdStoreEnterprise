using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.Services.Client.API.Data;

namespace NerdStoreEnterprise.Services.Client.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ClientsDbContext>();
        }
    }
}