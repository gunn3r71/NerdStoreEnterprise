using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Mediator;
using NerdStoreEnterprise.Services.Customer.API.Data;
using NerdStoreEnterprise.Services.Customer.API.Data.Repositories;
using NerdStoreEnterprise.Services.Customer.API.Infrastructure.Services;
using NerdStoreEnterprise.Services.Customer.API.Models;

namespace NerdStoreEnterprise.Services.Customer.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomersDbContext>();

            services.AddSingleton<IEmailService, EmailService>();
        }
    }
}