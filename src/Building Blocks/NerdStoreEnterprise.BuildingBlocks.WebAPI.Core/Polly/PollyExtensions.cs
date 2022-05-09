using System;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace NerdStoreEnterprise.BuildingBlocks.Services.Core.Polly
{
    public static class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> EnableWaitAndRetryPolicy()
        {
            var retryWaitPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, (retryAttempt) => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), 
                    (outcome, timespan, retryCount, context) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"trying for the {retryCount} time");
                        Console.ForegroundColor = ConsoleColor.White;
                    });

            return retryWaitPolicy;
        }
    }
}