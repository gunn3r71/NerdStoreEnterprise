using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NerdStoreEnterprise.Services.Cart.API.Data;

namespace NerdStoreEnterprise.Services.Cart.API.Infrastructure.Factories
{
    public class CartDbContextFactory : IDesignTimeDbContextFactory<CartDbContext>
    {
        public CartDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Development;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json")
                .Build();

            var connectionString = configuration.GetConnectionString("CartServiceConnection");

            var builder = new DbContextOptionsBuilder<CartDbContext>();

            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x =>
            {
                x.EnableRetryOnFailure(3);
                x.MigrationsHistoryTable("CartMigrations");
                x.CommandTimeout(15);
            });


            return new CartDbContext(builder.Options);
        }
    }
}