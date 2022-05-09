using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NerdStoreEnterprise.BuildingBlocks.Services.Core.OpenAPI.Filters
{
    public class StatusCodeDocumentationOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var successResponse = new OpenApiResponse() { Description = "Success" };

            operation.Responses.TryAdd("200", successResponse);
            operation.Responses.TryAdd("201", successResponse);
            operation.Responses.TryAdd("204", successResponse);

            operation.Responses.TryAdd("400", new OpenApiResponse() { Description = "Validation errors occurred" });
            operation.Responses.TryAdd("404", new OpenApiResponse() { Description = "Resource not found" });

            var allowAnonymous = context.MethodInfo.GetCustomAttribute(typeof(AllowAnonymousAttribute)) is not null;
            if (allowAnonymous) return;

            operation.Responses.TryAdd("401", new OpenApiResponse() { Description = "User isn't authenticated" });
            operation.Responses.TryAdd("403", new OpenApiResponse() { Description = "User doesn't have permission" });

        }
    }
}