using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace NerdStoreEnterprise.WebApp.Mvc.Extensions
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AspNetUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string Name => _contextAccessor.HttpContext?.User.Identity?.Name;

        public Guid GetUserId()
        {
            return Guid.TryParse(GetHttpContext()?.User?.GetUserId() ?? string.Empty, out var userId)
                ? userId
                : Guid.Empty;
        }

        public string GetUserEmail()
        {
            return GetHttpContext()?.User?.GetUserEmail();
        }

        public string GetUserToken()
        {
            return GetHttpContext()?.User?.GetUserToken();
        }

        public bool IsAuthenticated()
        {
            return GetHttpContext()?.User?.Identity?.IsAuthenticated ?? false;
        }

        public bool HasRole(string role)
        {
            return GetHttpContext()?.User?.IsInRole(role) ?? false;
        }

        public IEnumerable<Claim> GetUserClaims()
        {
            return GetHttpContext()?.User?.Claims ?? Enumerable.Empty<Claim>();
        }

        public HttpContext GetHttpContext()
        {
            return _contextAccessor.HttpContext;
        }
    }
}