using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.Identity;
using NerdStoreEnterprise.Services.Catalog.API.Infrastructure.Seeders;

namespace NerdStoreEnterprise.Services.Catalog.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomDatabase(configuration);

            services.RegisterServices();

            services.AddControllers();

            services.AddCustomAuthentication(configuration);

            services.AddCorsPolicies();

            services.AddCustomSwagger();
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env, IProductSeeder productSeeder)
        {
            app.UseCustomSwagger(env);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("FullAccess");

            productSeeder.Seed();

            app.UseCustomAuthentication();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
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