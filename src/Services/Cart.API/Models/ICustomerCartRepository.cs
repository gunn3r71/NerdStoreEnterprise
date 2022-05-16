using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Data;

namespace NerdStoreEnterprise.Services.Cart.API.Models
{
    public interface ICustomerCartRepository : IRepository<CustomerCart>
    {
        Task<CustomerCart> GetCustomerCart(Guid customerId);
        void AddCustomerCart(CustomerCart cart);
        void UpdateCustomerCart(CustomerCart cart);
        Task<CartItem> GetCartItem(Guid cartId, Guid productId);
        void AddCartItem(CartItem item);
        void UpdateCartItem(CartItem item);
    }
}