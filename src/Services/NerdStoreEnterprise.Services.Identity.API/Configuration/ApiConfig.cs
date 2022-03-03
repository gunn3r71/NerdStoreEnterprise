using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NerdStoreEnterprise.Services.Identity.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(x =>
                {
                    x.RegisterValidatorsFromAssemblyContaining<Startup>();
                    x.ValidatorOptions.LanguageManager.Enabled = false;
                });

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCustomIdentity();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}