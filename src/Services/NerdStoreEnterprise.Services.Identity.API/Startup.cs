using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStoreEnterprise.Services.Identity.API.Configuration;

namespace NerdStoreEnterprise.Services.Identity.API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services) => 
            services.AddApiConfiguration(Configuration);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) => 
            app.UseApiConfiguration(env);
    }
}
