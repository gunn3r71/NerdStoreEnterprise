using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace NerdStoreEnterprise.BuildingBlocks.Services.Core.Identity
{
    public static class AuthenticationConfig
    {
        /// <summary>
        ///     Add authentication configuration
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            var tokenSettingsSettings = configuration.GetSection("TokenSettings");
            services.Configure<TokenSettings>(tokenSettingsSettings);

            var tokenSettings = tokenSettingsSettings.Get<TokenSettings>();
            var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = tokenSettings.Audience,
                    ValidIssuer = tokenSettings.Issuer
                };
            });

            return services;
        }

        /// <summary>
        ///     Configure authentication in the application
        /// </summary>
        /// <param name="app">The application builder</param>
        /// <returns>Application builder</returns>
        public static IApplicationBuilder UseCustomAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}