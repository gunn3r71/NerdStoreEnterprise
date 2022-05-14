using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Mediator;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages.IntegrationEvents;
using NerdStoreEnterprise.BuildingBlocks.MessageBus;
using NerdStoreEnterprise.Services.Customer.API.Application.Commands;

namespace NerdStoreEnterprise.Services.Customer.API.Services
{
    public class CreateCustomerIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public CreateCustomerIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            SetResponder();

            return Task.CompletedTask;
        }

        private void OnConnect(object sender, ConnectedEventArgs e) => SetResponder();

        private void SetResponder()
        {
            _bus.RespondAsync<CreatedUserIntegrationEvent, ResponseMessage>(async request =>
                await CreateCustomer(request));

            _bus.AdvancedBus.Connected += OnConnect;
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