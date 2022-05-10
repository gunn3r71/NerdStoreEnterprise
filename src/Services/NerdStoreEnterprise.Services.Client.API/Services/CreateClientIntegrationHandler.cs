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
using Microsoft.Extensions.Options;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.EventBus;

namespace NerdStoreEnterprise.Services.Client.API.Services
{
    public class CreateClientIntegrationHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private IBus _bus;

        public CreateClientIntegrationHandler(IServiceProvider serviceProvider, IOptions<RabbitMq> rabbitMqConfig)
        {
            _serviceProvider = serviceProvider;
            var rabbitMq = rabbitMqConfig.Value;
            _bus = RabbitHutch.CreateBus($"host={rabbitMq.Host};virtualHost={rabbitMq.VHost};username={rabbitMq.User};password={rabbitMq.Password}");
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _bus.Rpc.RespondAsync<CreatedUserIntegrationEvent, ResponseMessage>(async request => 
                new ResponseMessage(await CreateClient(request)), cancellationToken);

            return Task.CompletedTask;
        }

        private async Task<ValidationResult> CreateClient(CreatedUserIntegrationEvent message)
        {
            var createClientCommand = new CreateClientCommand(message.Id, message.Name, message.Email, message.Cpf);

            using var scope = _serviceProvider.CreateScope();
            
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            var success = await mediator.SendCommand(createClientCommand);

            return success;
        }
    }
}