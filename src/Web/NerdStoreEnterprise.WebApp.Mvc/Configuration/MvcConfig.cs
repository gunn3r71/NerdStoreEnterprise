using System.Globalization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.WebApp.Mvc.Extensions;

namespace NerdStoreEnterprise.WebApp.Mvc.Configuration
{
    public static class WebAppConfig
    {
        public static void AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomAuthentication();

            services.RegisterServices(configuration);

            services.Configure<ServicesUrls>(configuration.GetSection("ServicesUrls"));

            services.AddFluentValidation(x =>
            {
                x.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
                x.LocalizationEnabled = false;
            });

            services.AddControllersWithViews();
        }

        public static void UseCustomMvc(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error/500");

            app.UseStatusCodePagesWithRedirects("/error/{0}");

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCustomAuthentication();

            var supportedCultures = new[] {new CultureInfo("pt-BR"), new CultureInfo("en-US")};
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            
            
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}