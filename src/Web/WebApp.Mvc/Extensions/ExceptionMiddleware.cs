using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NerdStoreEnterprise.WebApp.Mvc.Exceptions;
using Polly.CircuitBreaker;
using Refit;

namespace NerdStoreEnterprise.WebApp.Mvc.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomHttpResponseException ex)
            {
                HandleRequestException(context, ex.StatusCode);
            }
            catch (ValidationApiException ex)
            {
                HandleRequestException(context, ex.StatusCode);
            }
            catch (ApiException ex)
            {
                HandleRequestException(context, ex.StatusCode);
            }
            catch (BrokenCircuitException)
            {
                HandleCircuitBreakerExceptionAsync(context);
            }
        }

        private static void HandleRequestException(HttpContext context, HttpStatusCode statusCode)
        {
            if (statusCode is HttpStatusCode.Unauthorized)
            {
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
                return;
            }

            context.Response.StatusCode = (int) statusCode;
        }

        private static void HandleCircuitBreakerExceptionAsync(HttpContext context) => 
            context.Response.Redirect("/system-unavailable");
    }
}