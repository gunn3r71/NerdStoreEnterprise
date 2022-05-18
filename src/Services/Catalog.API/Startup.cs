using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.Services.Catalog.API.Configuration;
using NerdStoreEnterprise.Services.Catalog.API.Infrastructure.Seeders;

namespace NerdStoreEnterprise.Services.Catalog.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) => 
            services.AddApiConfiguration(Configuration);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IProductSeeder productSeeder) => 
            app.UseApiConfiguration(env, productSeeder);
    }
}