using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NerdStoreEnterprise.Services.Client.API.Data;

namespace NerdStoreEnterprise.Services.Client.API.Infrastructure.Factories
{
    public class ClientsDbContextFactory : IDesignTimeDbContextFactory<ClientsDbContext>
    {
        public ClientsDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Development;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ClientsServiceConnection");

            var builder = new DbContextOptionsBuilder<ClientsDbContext>();

            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x =>
                {
                    x.EnableRetryOnFailure(3);
                    x.MigrationsHistoryTable("ClientsMigrations");
                    x.CommandTimeout(15);
                });

            return new ClientsDbContext(builder.Options);
        }
    }
}