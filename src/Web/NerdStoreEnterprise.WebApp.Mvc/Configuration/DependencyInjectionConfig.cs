using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.WebApp.Mvc.Extensions;
using NerdStoreEnterprise.WebApp.Mvc.Services;
using NerdStoreEnterprise.WebApp.Mvc.Services.Handlers;

namespace NerdStoreEnterprise.WebApp.Mvc.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            
            services.AddHttpClient<IAuthenticationService, AuthenticationService>();
            
            services.AddHttpClient<ICatalogService, CatalogService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
            
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}