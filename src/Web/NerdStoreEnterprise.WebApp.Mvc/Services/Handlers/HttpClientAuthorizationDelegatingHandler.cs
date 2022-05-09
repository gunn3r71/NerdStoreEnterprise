using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using NerdStoreEnterprise.WebApp.Mvc.Extensions;

namespace NerdStoreEnterprise.WebApp.Mvc.Services.Handlers
{
    public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IUser _user;

        public HttpClientAuthorizationDelegatingHandler(IUser user)
        {
            _user = user;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
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