using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NerdStoreEnterprise.BuildingBlocks.Services.Core.EventBus
{
    public static class RabbitMqConfig
    {
        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMq>(configuration.GetSection("RabbitMQ"));
        }
    }
}