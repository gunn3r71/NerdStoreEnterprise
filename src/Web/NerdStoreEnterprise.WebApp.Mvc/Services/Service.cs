using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NerdStoreEnterprise.WebApp.Mvc.Exceptions;

namespace NerdStoreEnterprise.WebApp.Mvc.Services
{
    public abstract class Service
    {
        protected static StringContent GenerateContent(object data)
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }

        protected async Task<T> DeserializeResponseAsync<T>(HttpResponseMessage response)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), options);
        }

        protected static bool HandleResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.InternalServerError:
                    throw new CustomHttpResponseException(response.StatusCode);

                case HttpStatusCode.BadRequest:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
