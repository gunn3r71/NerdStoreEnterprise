using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.BuildingBlocks.Core.Data;
using NerdStoreEnterprise.BuildingBlocks.WebAPI.Core.EF;
using NerdStoreEnterprise.Services.Catalog.API.Models;

namespace NerdStoreEnterprise.Services.Catalog.API.Data
{
    public class CatalogDbContext : DbContext, IUnitOfWork
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUnmappedStrings();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        }

        public async Task<bool> CommitAsync() => 
            await base.SaveChangesAsync() > 0;
    }
}