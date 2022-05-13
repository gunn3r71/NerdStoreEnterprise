using System;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Formatters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NerdStoreEnterprise.Services.Identity.API.Configuration
{
    public static class LogConfig
    {
        public static void AddCustomLogging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(logging =>
            {
                logging.AddKissLog(options =>
                {
                    options.Formatter = args =>
                    {
                        if (args.Exception is null) return args.DefaultValue;

                        var exception = new ExceptionFormatter().Format(args.Exception, args.Logger);

                        return string.Join(Environment.NewLine, new[] { args.DefaultValue, exception });
                    };
                });
            });
        }

        public static void UseCustomLogging(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseKissLogMiddleware(options => ConfigureKissLog(options, configuration));
        }

        private static void ConfigureKissLog(IOptionsBuilder options, IConfiguration configuration)
        {
            var kissLogConfig = configuration.GetSection("KissLog");

            var application = new Application(kissLogConfig["OrganizationId"], kissLogConfig["ApplicationId"]);

            var listener = new RequestLogsApiListener(application)
            {
                ApiUrl = kissLogConfig["ApiUrl"]
            };

            KissLogConfiguration.Listeners.Add(listener);
        }
    }
}