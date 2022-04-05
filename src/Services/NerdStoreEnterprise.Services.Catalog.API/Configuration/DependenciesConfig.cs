using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.Services.Catalog.API.Data;
using NerdStoreEnterprise.Services.Catalog.API.Data.Repositories;
using NerdStoreEnterprise.Services.Catalog.API.Models;

namespace NerdStoreEnterprise.Services.Catalog.API.Configuration
{
    public static class DependenciesConfig
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CatalogDbContext>();
        }
    }
}