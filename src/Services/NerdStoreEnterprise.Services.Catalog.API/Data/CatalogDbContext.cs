using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.Services.Catalog.API.Models;
using System.Threading.Tasks;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Data;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.EF;

namespace NerdStoreEnterprise.Services.Catalog.API.Data
{
    public class CatalogDbContext : DbContext, IUnitOfWork
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.IgnoreDomainMessageItems();

            builder.ConfigureUnmappedStrings();

            builder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        }

        public async Task<bool> CommitAsync() => await base.SaveChangesAsync() > 0;
    }
}