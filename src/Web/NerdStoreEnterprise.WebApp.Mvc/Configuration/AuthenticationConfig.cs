using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NerdStoreEnterprise.WebApp.Mvc.Configuration
{
    public static class AuthenticationConfig
    {
        public static void AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(x =>
                    {
                        x.LoginPath = "/login";
                        x.LogoutPath = "/logout";
                        x.AccessDeniedPath = "/access-denied";
                    });
        }

        public static void UseCustomAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}