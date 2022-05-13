using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Mediator;
using NerdStoreEnterprise.Services.Client.API.Data;
using NerdStoreEnterprise.Services.Client.API.Data.Repositories;
using NerdStoreEnterprise.Services.Client.API.Infrastructure.Services;
using NerdStoreEnterprise.Services.Client.API.Models;

namespace NerdStoreEnterprise.Services.Client.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ClientsDbContext>();

            services.AddSingleton<IEmailService, EmailService>();
        }
    }
}