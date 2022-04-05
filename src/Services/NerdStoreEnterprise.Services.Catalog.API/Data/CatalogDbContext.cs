using System.Linq;
using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.Services.Catalog.API.Models;

namespace NerdStoreEnterprise.Services.Catalog.API.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUnmappedStrings(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        }

        private static void ConfigureUnmappedStrings(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
        }
    }
}