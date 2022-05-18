using System;
using System.Threading.Tasks;
using NerdStoreEnterprise.WebApp.Mvc.Models.Cart;
using NerdStoreEnterprise.WebApp.Mvc.Models.Errors;

namespace NerdStoreEnterprise.WebApp.Mvc.Services
{
    public interface ICartService
    {
        Task<CartViewModel> GetCart();
        Task<ResponseResult> AddCartItem(CartItemViewModel cartItem);
        Task<ResponseResult> UpdateCartItem(Guid productId, CartItemViewModel cartItem);
        Task<ResponseResult> DeleteCartItem(Guid productId);
    }
}