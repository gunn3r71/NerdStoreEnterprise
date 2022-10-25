using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.User;
using NerdStoreEnterprise.Services.Cart.API.Data;

namespace NerdStoreEnterprise.Services.Cart.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLoggedUserDependencies();
            services.AddScoped<CartDbContext>();
        }
    }
}