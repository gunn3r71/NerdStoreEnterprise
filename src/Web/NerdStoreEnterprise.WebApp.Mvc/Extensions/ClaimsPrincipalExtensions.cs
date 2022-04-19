using System;
using System.Security.Claims;

namespace NerdStoreEnterprise.WebApp.Mvc.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal is null) throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst("sub")?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal is null) throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst("email")?.Value;
        }

        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            if (principal is null) throw new ArgumentNullException(nameof(principal));
            
            return principal.FindFirst("JWT")?.Value;
        }
    }
}