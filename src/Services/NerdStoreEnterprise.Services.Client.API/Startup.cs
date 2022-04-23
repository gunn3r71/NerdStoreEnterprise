using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NerdStoreEnterprise.Services.Client.API.Configuration;

namespace NerdStoreEnterprise.Services.Client.API
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
            app.UseApiConfiguration(env);
    }
}
