using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using NerdStoreEnterprise.BuildingBlocks.Services.Core;

namespace NerdStoreEnterprise.Services.Payment.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Development;
            var contentRootPath = Directory.GetCurrentDirectory();

            await ServiceHost.Create<Startup>(args, env, contentRootPath)
                .Build()
                .RunAsync();
        }
    }
}
