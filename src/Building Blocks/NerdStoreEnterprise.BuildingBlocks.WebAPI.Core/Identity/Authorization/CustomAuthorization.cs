using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace NerdStoreEnterprise.BuildingBlocks.WebAPI.Core.Identity.Authorization
{
    public static class CustomAuthorization
    {
        public static bool ValidateUserClaims(HttpContext context, Claim requiredClaim)
        {
            var isAuthenticated = context.User.Identity is {IsAuthenticated: true};

            var hasClaim = context.User.HasClaim(claim =>
                claim.Type.Equals(requiredClaim.Type, StringComparison.InvariantCultureIgnoreCase) &&
                claim.Value.Contains(requiredClaim.Value));

            return isAuthenticated && hasClaim;
        }
    }
}