using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.BuildingBlocks.WebAPI.Core
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;

        public ServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }

        public async Task RunAsync() => await _webHost.RunAsync();

        public static HostBuilder Create<TStartup>(string[] args, string env, string contentRootPath) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace ?? throw new InvalidOperationException("namespace not informed.");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(contentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables();

            if (env.Contains("Development")) configuration.AddUserSecrets<TStartup>();

            var builder = WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(x =>
                {
                    x.ClearProviders();
                    x.AddConsole();
                })
                .UseDefaultServiceProvider(x => x.ValidateScopes = false)
                .UseConfiguration(configuration.Build())
                .UseStartup<TStartup>();

            return new HostBuilder(builder.Build());
        }
    }

    public abstract class BuilderBase
    {
        public abstract ServiceHost Build();
    }

    public class HostBuilder : BuilderBase
    {
        private readonly IWebHost _webHost;

        public HostBuilder(IWebHost webHost)
        {
            _webHost = webHost;
        }

        public override ServiceHost Build() => new(_webHost);
    }
}