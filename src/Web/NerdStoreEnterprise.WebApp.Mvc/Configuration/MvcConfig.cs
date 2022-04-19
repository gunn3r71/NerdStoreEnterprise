using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.WebApp.Mvc.Extensions;

namespace NerdStoreEnterprise.WebApp.Mvc.Configuration
{
    public static class WebAppConfig
    {
        public static void AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServicesUrls>(configuration.GetSection("ServicesUrls"));

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