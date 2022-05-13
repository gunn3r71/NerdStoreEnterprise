using System;
using Microsoft.Extensions.DependencyInjection;
namespace NerdStoreEnterprise.BuildingBlocks.MessageBus
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            services.AddSingleton<IMessageBus>(new MessageBus(connectionString));

            return services;
        }
    }
}