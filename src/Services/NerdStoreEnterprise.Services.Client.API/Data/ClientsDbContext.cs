using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.EF;

namespace NerdStoreEnterprise.Services.Client.API.Data
{
    public sealed class ClientsDbContext : DbContext
    {
        public ClientsDbContext(DbContextOptions<ClientsDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        
        public DbSet<Models.Client> Clients { get; set; }
        public DbSet<Models.Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ConfigureUnmappedStrings();
            builder.DisableDeleteBehavior();
            builder.ApplyConfigurationsFromAssembly(typeof(ClientsDbContext).Assembly);
        }
    }
}