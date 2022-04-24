using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.BuildingBlocks.Core.Data;
using NerdStoreEnterprise.BuildingBlocks.WebAPI.Core.EF;

namespace NerdStoreEnterprise.Services.Client.API.Data
{
    public class ClientsDbContext : DbContext, IUnitOfWork
    {
        public ClientsDbContext(DbContextOptions<ClientsDbContext> options) : base(options)
        {
            DisableQueryTrackingBehaviorAndChangesDetector();
        }

        public DbSet<Models.Client> Clients { get; set; }
        public DbSet<Models.Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUnmappedStrings();
            modelBuilder.DisableDeleteBehavior();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientsDbContext).Assembly);
        }

        public async Task<bool> CommitAsync() => await base.SaveChangesAsync() > 0;

        private void DisableQueryTrackingBehaviorAndChangesDetector()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
    }
}