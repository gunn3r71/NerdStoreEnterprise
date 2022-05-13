using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.Services.Identity.API.Data;

namespace NerdStoreEnterprise.Services.Identity.API.Configuration
{
    public static class DatabaseConfig
    {
        public static void AddCustomDatabase(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(o =>
            {
                var connectionString = configuration.GetConnectionString("IdentityServiceConnection");

                o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x =>
                {
                    x.EnableRetryOnFailure(3);
                    x.MigrationsHistoryTable("IdentityMigrations");
                    x.CommandTimeout(15);
                });
            });
        }
    }
}