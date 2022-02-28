using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NerdStoreEnterprise.Services.Identity.API.Configuration
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            var openApiInfo = new OpenApiInfo
            {
                Title = "NerdStoreEnterprise.Services.Identity.API",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Email = "lucas.p.oliveira@outlook.pt",
                    Name = "Lucas Pereira"
                },
                Description = "API to provide user authentication services"
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", openApiInfo);
            });

            return services;
        }
    }
}
