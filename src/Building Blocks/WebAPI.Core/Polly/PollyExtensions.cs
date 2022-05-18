using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace NerdStoreEnterprise.BuildingBlocks.Services.Core.Polly
{
    public static class PollyExtensions
    {
        /// <summary>
        /// Will add a retry policy
        /// </summary>
        /// <param name="builder">A builder for configuring HttpClient</param>
        /// <param name="policy">The policy to be executed</param>
        /// <param name="numberOfExceptionsAllowed">The number of exceptions allowed (default 2)</param>
        /// <param name="durationOfBreakInSeconds">The duration of break in seconds (default 30)</param>
        /// <returns></returns>
        public static IHttpClientBuilder AddPolicy(this IHttpClientBuilder builder, IAsyncPolicy<HttpResponseMessage> policy, int numberOfExceptionsAllowed = 2, int durationOfBreakInSeconds = 30) =>
            builder.AddPolicyHandler(policy)
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(numberOfExceptionsAllowed, TimeSpan.FromSeconds(durationOfBreakInSeconds)));

        /// <summary>
        /// Will enable wait and retry policy
        /// </summary>
        /// <returns>A policy</returns>
        public static AsyncRetryPolicy<HttpResponseMessage> EnableWaitAndRetryPolicy()
        {
            var retryWaitPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
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