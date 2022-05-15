using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NerdStoreEnterprise.Services.Identity.API.Data;

namespace NerdStoreEnterprise.Services.Identity.API.Infrastructure.Factories
{
    public class IdentityDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Development;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json")
                .Build();

            var connectionString = configuration.GetConnectionString("IdentityServiceConnection");

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x =>
            {
                x.EnableRetryOnFailure(3);
                x.MigrationsHistoryTable("IdentityMigrations");
                x.CommandTimeout(15);
            });

            return new ApplicationDbContext(builder.Options);
        }
    }
}