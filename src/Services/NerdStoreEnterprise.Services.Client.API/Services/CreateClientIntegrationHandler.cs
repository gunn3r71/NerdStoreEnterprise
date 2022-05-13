using EasyNetQ;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Mediator;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages.IntegrationEvents;
using NerdStoreEnterprise.Services.Client.API.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using NerdStoreEnterprise.BuildingBlocks.MessageBus;

namespace NerdStoreEnterprise.Services.Client.API.Services
{
    public class CreateClientIntegrationHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageBus _bus;

        public CreateClientIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _bus.RespondAsync<CreatedUserIntegrationEvent, ResponseMessage>(async request => await CreateClient(request));

            return Task.CompletedTask;
        }

        private async Task<ResponseMessage> CreateClient(CreatedUserIntegrationEvent message)
        {
            var createClientCommand = new CreateClientCommand(message.Id, message.Name, message.Email, message.Cpf);

            using var scope = _serviceProvider.CreateScope();
            
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            var success = await mediator.SendCommand(createClientCommand);

            return new ResponseMessage(success);
        }
    }
}