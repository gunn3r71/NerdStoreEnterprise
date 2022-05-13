using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Tools;
using NerdStoreEnterprise.BuildingBlocks.MessageBus;
using NerdStoreEnterprise.Services.Customer.API.Services;

namespace NerdStoreEnterprise.Services.Customer.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<CreateCustomerIntegrationHandler>();
        }
    }
}