using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NerdStoreEnterprise.WebApp.Mvc.Extensions;

namespace NerdStoreEnterprise.WebApp.Mvc.Configuration
{
    public static class WebAppConfig
    {
        public static void AddCustomMvc(this IServiceCollection services)
        {
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