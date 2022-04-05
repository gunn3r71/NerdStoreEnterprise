using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NerdStoreEnterprise.Services.Catalog.API.Configuration;

namespace NerdStoreEnterprise.Services.Catalog.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false, true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment()) builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) =>
            services.AddApiConfiguration(Configuration);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
            app.UseApiConfiguration(env);
    }
}
