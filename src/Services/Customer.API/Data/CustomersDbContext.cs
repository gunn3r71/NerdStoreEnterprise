using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Data;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Mediator;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.EF;
using NerdStoreEnterprise.Services.Customer.API.Extensions;
using NerdStoreEnterprise.Services.Customer.API.Models;

namespace NerdStoreEnterprise.Services.Customer.API.Data
{
    public sealed class CustomersDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public CustomersDbContext(DbContextOptions<CustomersDbContext> options, IMediatorHandler mediatorHandler) :
            this(options)
        {
            _mediatorHandler = mediatorHandler ?? throw new ArgumentNullException(nameof(mediatorHandler));
        }

        public DbSet<Models.Customer> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public async Task<bool> CommitAsync()
        {
            var success = await SaveChangesAsync() > 0;

            if (success) await _mediatorHandler.PublishEventsAsync(this);

            return success;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.IgnoreDomainMessageItems();
            builder.ConfigureUnmappedStrings();
            //builder.DisableDeleteBehavior();
            builder.ApplyConfigurationsFromAssembly(typeof(CustomersDbContext).Assembly);
        }
    }
}