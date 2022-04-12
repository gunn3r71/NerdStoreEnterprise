﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace NerdStoreEnterprise.Services.Catalog.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddCustomSwagger(this IServiceCollection services)
        {
            var openApiInfo = new OpenApiInfo
            {
                Title = "NerdStoreEnterprise.Services.Catalog.API",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Email = "lucas.p.oliveira@outlook.pt",
                    Name = "Lucas Pereira"
                },
                Description = "API to provide catalog services"
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", openApiInfo);
            });
        }

        public static void UseCustomSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment()) return;

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NerdStoreEnterprise.Services.Catalog.API v1"));
        }
    }
}
