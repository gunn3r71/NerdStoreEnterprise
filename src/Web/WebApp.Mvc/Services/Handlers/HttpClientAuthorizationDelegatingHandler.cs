using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.User;

namespace NerdStoreEnterprise.WebApp.Mvc.Services.Handlers
{
    public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IAspNetUser _user;

        public HttpClientAuthorizationDelegatingHandler(IAspNetUser user)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var authHeader = _user.GetHttpContext().Request.Headers["Authorization"];

            if (!string.IsNullOrWhiteSpace(authHeader)) return base.SendAsync(request, cancellationToken);

            var token = _user.GetUserToken();

            if (!string.IsNullOrWhiteSpace(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return base.SendAsync(request, cancellationToken);
        }
    }
}