﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.EventBus;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.Identity;

namespace NerdStoreEnterprise.Services.Client.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomDatabase(configuration);

            services.AddMediatR(typeof(Startup));

            services.RegisterServices();

            services.AddCustomAuthentication(configuration);

            services.AddRabbitMq(configuration);

            services.AddControllers();

            services.AddCorsPolicies();

            services.AddCustomSwagger();
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomSwagger(env);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("FullAccess");

            app.UseCustomAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddCorsPolicies(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("FullAccess", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }
    }
}