using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.Identity;

namespace NerdStoreEnterprise.Services.Identity.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomDatabase(configuration);

            services.AddCustomIdentity();

            services.AddCustomAuthentication(configuration);

            services.AddControllers()
                .AddFluentValidation(x =>
                {
                    x.RegisterValidatorsFromAssemblyContaining<Startup>();
                    x.ValidatorOptions.LanguageManager.Enabled = false;
                });

            services.AddCustomSwagger();
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomSwagger(env);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCustomAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}