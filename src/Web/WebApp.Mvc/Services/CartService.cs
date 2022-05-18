using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NerdStoreEnterprise.WebApp.Mvc.Extensions;
using NerdStoreEnterprise.WebApp.Mvc.Models.Cart;
using NerdStoreEnterprise.WebApp.Mvc.Models.Errors;

namespace NerdStoreEnterprise.WebApp.Mvc.Services
{
    public class CartService : Service, ICartService
    {
        private readonly HttpClient _client;

        public CartService(HttpClient client, IOptions<ServicesUrls> servicesUrls)
        {
            client.BaseAddress = new Uri(servicesUrls.Value.CartUrl);
            _client = client;
        }

        public async Task<CartViewModel> GetCart()
        {
            var response = await _client.GetAsync("api/v1/cart");

            HandleResponse(response);

            return await DeserializeResponseAsync<CartViewModel>(response);
        }

        public async Task<ResponseResult> AddCartItem(CartItemViewModel cartItem)
        {
            var content = GenerateContent(cartItem);

            var response = await _client.PostAsync("api/v1/cart/item", content);

            if (HandleResponse(response)) return ReturnOk();

            return await DeserializeResponseAsync<ResponseResult>(response);
        }

        public async Task<ResponseResult> UpdateCartItem(Guid productId, CartItemViewModel cartItem)
        {
            var content = GenerateContent(cartItem);
            
            var response = await _client.PutAsync($"api/v1/cart/item/{productId}", content);

            if (HandleResponse(response)) return ReturnOk();

            return await DeserializeResponseAsync<ResponseResult>(response);
        }

        public async Task<ResponseResult> DeleteCartItem(Guid productId)
        {
            var response = await _client.DeleteAsync($"api/v1/cart/item/{productId}");

            if (HandleResponse(response)) return ReturnOk();

            return await DeserializeResponseAsync<ResponseResult>(response);
        }
    }
}