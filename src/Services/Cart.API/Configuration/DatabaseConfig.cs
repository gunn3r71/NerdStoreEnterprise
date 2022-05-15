using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.Services.Cart.API.Data;

namespace NerdStoreEnterprise.Services.Cart.API.Configuration
{
    public static class DatabaseConfig
    {
        public static void AddCustomDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CartDbContext>(o =>
            {
                var connectionString = configuration.GetConnectionString("CartServiceConnection");

                o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x =>
                {
                    x.EnableRetryOnFailure(3);
                    x.MigrationsHistoryTable("CartMigrations");
                    x.CommandTimeout(15);
                });
            });
        }
    }
}