using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NerdStoreEnterprise.Services.Catalog.API.Data;

namespace NerdStoreEnterprise.Services.Catalog.API.Infrastructure.Factories
{
    public class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
    {
        public CatalogDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Development;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json")
                .Build();

            var connectionString = configuration.GetConnectionString("CatalogServiceConnection");

            var builder = new DbContextOptionsBuilder<CatalogDbContext>();

            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x =>
            {
                x.EnableRetryOnFailure(3);
                x.MigrationsHistoryTable("CatalogMigrations");
                x.CommandTimeout(15);
            });

            return new CatalogDbContext(builder.Options);
        }
    }
}