using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Mediator;

namespace NerdStoreEnterprise.Services.Customer.API.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task PublishEventsAsync<T>(this IMediatorHandler mediator, T context) where T : DbContext
        {
            var domainEntities = context.ChangeTracker.Entries<Entity>()
                .Where(x => x.Entity.Events is not null && x.Entity.Events.Any()).ToList();

            var domainEvents = domainEntities.SelectMany(x => x.Entity.Events).ToList();

            domainEntities.ForEach(x => x.Entity.ClearEvents());

            var tasks = domainEvents.Select(async domainEvent => { await mediator.PublishEventAsync(domainEvent); });

            await Task.WhenAll(tasks);
        }
    }
}