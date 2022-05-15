using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.EF;
using NerdStoreEnterprise.Services.Cart.API.Models;

namespace NerdStoreEnterprise.Services.Cart.API.Data
{
    public sealed class CartDbContext : DbContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CustomerCart> CustomerCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ConfigureUnmappedStrings();
            //builder.DisableDeleteBehavior();
            builder.IgnoreDomainMessageItems();

            builder.ApplyConfigurationsFromAssembly(typeof(CartDbContext).Assembly);
        }
    }
}