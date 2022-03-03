using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NerdStoreEnterprise.Services.Identity.API.Extensions;

namespace NerdStoreEnterprise.Services.Identity.API.Configuration
{
    public static class AuthenticationConfig
    {
        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
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
        }
    }
}