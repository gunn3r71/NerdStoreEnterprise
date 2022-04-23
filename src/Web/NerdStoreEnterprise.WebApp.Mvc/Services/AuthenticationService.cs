using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NerdStoreEnterprise.WebApp.Mvc.Extensions;
using NerdStoreEnterprise.WebApp.Mvc.Models.AuthenticationResponse;
using NerdStoreEnterprise.WebApp.Mvc.Models.Errors;
using NerdStoreEnterprise.WebApp.Mvc.Models.Users;

namespace NerdStoreEnterprise.WebApp.Mvc.Services
{
    public class AuthenticationService : Service, IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient, IOptions<ServicesUrls> services)
        {
            httpClient.BaseAddress = new Uri(services.Value.AuthenticationUrl);
            _httpClient = httpClient;
        }

        public async Task<TokenViewModel> Login(UserLoginViewModel login)
        {
            var content = GenerateContent(login);

            var response = await _httpClient.PostAsync("/api/v1/account/authenticate", content);

            if (!HandleResponse(response))
            {
                return new TokenViewModel
                {
                    ErrorDetails = await DeserializeResponseAsync<ErrorViewModel>(response)
                };
            }

            return await DeserializeResponseAsync<TokenViewModel>(response);
        }

        public async Task<TokenViewModel> Register(UserRegisterViewModel register)
        {
            var content = GenerateContent(register);

            var response = await _httpClient.PostAsync("/api/v1/account/register", content);

            if (!HandleResponse(response))
            {
                return new TokenViewModel
                {
                    ErrorDetails = await DeserializeResponseAsync<ErrorViewModel>(response)
                };
            }

            return await DeserializeResponseAsync<TokenViewModel>(response);
        }
    }
}