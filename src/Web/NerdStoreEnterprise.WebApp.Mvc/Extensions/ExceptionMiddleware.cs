using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NerdStoreEnterprise.WebApp.Mvc.Exceptions;

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
                HandleRequestException(context, ex);
            }
        }

        private static void HandleRequestException(HttpContext context, CustomHttpResponseException exception)
        {
            if (exception.StatusCode is HttpStatusCode.Unauthorized)
            {
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
                return;
            }

            context.Response.StatusCode = (int) exception.StatusCode;
        }
    }
}