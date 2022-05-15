using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Mediator;
using NerdStoreEnterprise.BuildingBlocks.EmailSender;
using NerdStoreEnterprise.Services.Customer.API.Data;
using NerdStoreEnterprise.Services.Customer.API.Data.Repositories;
using NerdStoreEnterprise.Services.Customer.API.Models;

namespace NerdStoreEnterprise.Services.Customer.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<CustomersDbContext>();

            services.AddEmailSender(configuration);
        }
    }
}