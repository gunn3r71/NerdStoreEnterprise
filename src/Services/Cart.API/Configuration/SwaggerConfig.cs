using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.OpenAPI.Filters;

namespace NerdStoreEnterprise.Services.Cart.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddCustomSwagger(this IServiceCollection services)
        {
            var openApiInfo = new OpenApiInfo
            {
                Title = "NerdStoreEnterprise.Services.Cart.API",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Email = "lucas.p.oliveira@outlook.pt",
                    Name = "Lucas Pereira"
                },
                Description = "API to provide cart services"
            };

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<StatusCodeDocumentationOperationFilter>();
                c.SwaggerDoc("v1", openApiInfo);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "enter your token this way: Bearer {token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void UseCustomSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment()) return;

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NerdStoreEnterprise.Services.Catalog.API v1"));
        }
    }
}