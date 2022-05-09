using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NerdStoreEnterprise.BuildingBlocks.Services.Core.Identity.Authorization
{
    public class RequirementFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RequirementFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity is {IsAuthenticated: false})
            {
                context.Result = new StatusCodeResult((int) HttpStatusCode.Unauthorized);
                return;
            }

            if (!CustomAuthorization.ValidateUserClaims(context.HttpContext, _claim))
                context.Result = new StatusCodeResult((int) HttpStatusCode.Forbidden);
        }
    }
}