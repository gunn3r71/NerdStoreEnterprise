using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.Services.Identity.API.Data;

namespace NerdStoreEnterprise.Services.Identity.API.Configuration
{
    public static class IdentityConfig
    {
        public static void AddCustomIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>(ConfigureIdentityOptions)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        private static void ConfigureIdentityOptions(IdentityOptions identityOptions)
        {
            identityOptions.Lockout = new LockoutOptions
            {
                AllowedForNewUsers = true,
                MaxFailedAccessAttempts = 5,
                DefaultLockoutTimeSpan = TimeSpan.FromHours(2)
            };

            identityOptions.Password = new PasswordOptions
            {
                RequireDigit = true,
                RequiredLength = 8,
                RequireNonAlphanumeric = true,
                RequireUppercase = true,
                RequireLowercase = true
            };

            identityOptions.User = new UserOptions
            {
                RequireUniqueEmail = true
            };
        }
    }
}