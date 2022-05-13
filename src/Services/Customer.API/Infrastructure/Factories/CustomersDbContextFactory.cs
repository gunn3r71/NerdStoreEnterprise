using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NerdStoreEnterprise.Services.Customer.API.Data;

namespace NerdStoreEnterprise.Services.Customer.API.Infrastructure.Factories
{
    public class CustomersDbContextFactory : IDesignTimeDbContextFactory<CustomersDbContext>
    {
        public CustomersDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Development;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json")
                .Build();

            var connectionString = configuration.GetConnectionString("CustomersServiceConnection");

            var builder = new DbContextOptionsBuilder<CustomersDbContext>();

            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x =>
            {
                x.EnableRetryOnFailure(3);
                x.MigrationsHistoryTable("CustomersMigrations");
                x.CommandTimeout(15);
            });

            return new CustomersDbContext(builder.Options);
        }
    }
}