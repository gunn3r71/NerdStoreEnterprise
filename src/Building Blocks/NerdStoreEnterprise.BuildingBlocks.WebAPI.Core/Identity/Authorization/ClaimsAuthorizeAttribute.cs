using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace NerdStoreEnterprise.BuildingBlocks.Services.Core.Identity.Authorization
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequirementFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}