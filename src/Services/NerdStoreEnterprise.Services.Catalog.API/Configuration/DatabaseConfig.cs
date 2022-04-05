using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.Services.Catalog.API.Data;

namespace NerdStoreEnterprise.Services.Catalog.API.Configuration
{
    public static class DatabaseConfig
    {
        public static void AddCustomDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(o =>
            {
                var connectionString = configuration.GetConnectionString("CatalogServiceConnection");

                o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x =>
                {
                    x.EnableRetryOnFailure(3);
                    x.MigrationsHistoryTable("CatalogMigrations");
                    x.CommandTimeout(15);
                });
            });
        }
    }
}
