using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.OpenAPI.Filters;

namespace NerdStoreEnterprise.Services.Identity.API.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void AddCustomSwagger(this IServiceCollection services)
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
                c.OperationFilter<StatusCodeDocumentationOperationFilter>();
                c.SwaggerDoc("v1", openApiInfo);
            });
        }

        public static void UseCustomSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment()) return;

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NerdStoreEnterprise.Services.Identity.API v1"));
        }
    }
}