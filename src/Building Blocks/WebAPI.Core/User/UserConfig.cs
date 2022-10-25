using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace NerdStoreEnterprise.BuildingBlocks.Services.Core.User
{
    public static class UserConfig
    {
        public static IServiceCollection AddLoggedUserDependencies(this IServiceCollection services)
        {
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, User>();

            return services;
        }
    }
}