using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.WebApp.Mvc.Extensions;
using NerdStoreEnterprise.WebApp.Mvc.Services;
using NerdStoreEnterprise.WebApp.Mvc.Services.Handlers;

namespace NerdStoreEnterprise.WebApp.Mvc.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            var servicesUrls = new ServicesUrls();
            configuration.GetSection("ServicesUrls").Bind(servicesUrls);
            
            services.AddHttpClient<IAuthenticationService, AuthenticationService>();

            services.RegisterHttpClient("catalog", servicesUrls.CatalogUrl)
                .AddTypedClient(Refit.RestService.For<ICatalogService>);
            
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped<IUser, AspNetUser>();
        }

        private static IHttpClientBuilder RegisterHttpClient(this IServiceCollection services, string httpClientName, string url) =>
            services.AddHttpClient(httpClientName, options =>
                {
                    options.BaseAddress = new Uri($"{url}/api/v1");
                    options.Timeout = TimeSpan.FromSeconds(30);
                })
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
    }
}