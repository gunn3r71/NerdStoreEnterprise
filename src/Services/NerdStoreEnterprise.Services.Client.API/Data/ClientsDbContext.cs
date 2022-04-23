using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.BuildingBlocks.Core.Data;
using NerdStoreEnterprise.BuildingBlocks.WebAPI.Core.EF;

namespace NerdStoreEnterprise.Services.Client.API.Data
{
    public sealed class ClientsDbContext : DbContext, IUnitOfWork
    {
        public ClientsDbContext(DbContextOptions<ClientsDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Models.Client> Clients { get; set; }
        public DbSet<Models.Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUnmappedStrings();
            modelBuilder.DisableDeleteBehavior();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientsDbContext).Assembly);
        }

        public async Task<bool> CommitAsync() => await SaveChangesAsync() > 0;
    }
}