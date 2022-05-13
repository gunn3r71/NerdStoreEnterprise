using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Mediator;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages.IntegrationEvents;
using NerdStoreEnterprise.BuildingBlocks.MessageBus;
using NerdStoreEnterprise.Services.Customer.API.Application.Commands;

namespace NerdStoreEnterprise.Services.Customer.API.Services
{
    public class CreateCustomerIntegrationHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageBus _bus;

        public CreateCustomerIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _bus.RespondAsync<CreatedUserIntegrationEvent, ResponseMessage>(async request => await CreateCustomer(request));

            return Task.CompletedTask;
        }

        private async Task<ResponseMessage> CreateCustomer(CreatedUserIntegrationEvent message)
        {
            var createCustomerCommand = new CreateCustomerCommand(message.Id, message.Name, message.Email, message.Cpf);

            using var scope = _serviceProvider.CreateScope();
            
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            var success = await mediator.SendCommand(createCustomerCommand);

            return new ResponseMessage(success);
        }
    }
}