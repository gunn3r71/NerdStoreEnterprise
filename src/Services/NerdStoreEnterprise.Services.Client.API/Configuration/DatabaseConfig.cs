using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.Services.Client.API.Data;

namespace NerdStoreEnterprise.Services.Client.API.Configuration
{
    public static class DatabaseConfig
    {
        public static void AddCustomDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ClientsDbContext>(o =>
            {
                var connectionString = configuration.GetConnectionString("ClientServiceConnection");

                o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x =>
                {
                    x.EnableRetryOnFailure(3);
                    x.MigrationsHistoryTable("ClientMigrations");
                    x.CommandTimeout(15);
                });
            });
        }
    }
}
