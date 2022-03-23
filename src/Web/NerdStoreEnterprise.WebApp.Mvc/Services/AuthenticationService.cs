using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NerdStoreEnterprise.WebApp.Mvc.Models.AuthenticationResponse;
using NerdStoreEnterprise.WebApp.Mvc.Models.Errors;
using NerdStoreEnterprise.WebApp.Mvc.Models.Users;

namespace NerdStoreEnterprise.WebApp.Mvc.Services
{
    public class AuthenticationService : Service, IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TokenViewModel> Login(UserLoginViewModel login)
        {
            var content = new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:5001/api/v1/account/authenticate", content);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            
            if (!IsSuccess(response))
            {
                return new TokenViewModel
                {
                    ErrorDetails = JsonSerializer.Deserialize<ErrorViewModel>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<TokenViewModel>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<TokenViewModel> Register(UserRegisterViewModel register)
        {
            var content = new StringContent(JsonSerializer.Serialize(register), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:5001/api/v1/account/register", content);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (!IsSuccess(response))
            {
                return new TokenViewModel
                {
                    ErrorDetails = JsonSerializer.Deserialize<ErrorViewModel>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<TokenViewModel>(await response.Content.ReadAsStringAsync(), options);
        }
    }
}