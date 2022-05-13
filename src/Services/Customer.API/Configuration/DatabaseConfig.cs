using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.Services.Customer.API.Data;

namespace NerdStoreEnterprise.Services.Customer.API.Configuration
{
    public static class DatabaseConfig
    {
        public static void AddCustomDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomersDbContext>(o =>
            {
                var connectionString = configuration.GetConnectionString("CustomersServiceConnection");

                o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x =>
                {
                    x.EnableRetryOnFailure(3);
                    x.MigrationsHistoryTable("CustomersMigrations");
                    x.CommandTimeout(15);
                });
            });
        }
    }
}