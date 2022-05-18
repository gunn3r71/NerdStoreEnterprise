using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.WebApp.Mvc.Extensions;
using NerdStoreEnterprise.WebApp.Mvc.Services;
using NerdStoreEnterprise.WebApp.Mvc.Services.Handlers;
using Refit;
using System;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.User;
using static NerdStoreEnterprise.BuildingBlocks.Services.Core.Polly.PollyExtensions;

namespace NerdStoreEnterprise.WebApp.Mvc.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpServices(configuration);

            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IAspNetUser, AspNetUser>();
        }

        private static void AddHttpServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            var servicesUrls = new ServicesUrls();
            configuration.GetSection("ServicesUrls").Bind(servicesUrls);

            services.AddHttpClient<IAuthenticationService, AuthenticationService>()
                .AddPolicy(EnableWaitAndRetryPolicy());

            services.AddHttpClient<ICartService, CartService>()
                .AddPolicy(EnableWaitAndRetryPolicy())
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.RegisterHttpClient("Catalog", servicesUrls.CatalogUrl)
                .AddPolicy(EnableWaitAndRetryPolicy())
                .AddTypedClient(RestService.For<ICatalogService>);
        }

        private static IHttpClientBuilder RegisterHttpClient(this IServiceCollection services, string httpClientName, string url)
        {
            return services.AddHttpClient(httpClientName,
                    options => { options.BaseAddress = new Uri($"{url}/api/v1"); })
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
        }
    }
}