using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.Controllers;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.User;
using NerdStoreEnterprise.Services.Cart.API.Models;

namespace NerdStoreEnterprise.Services.Cart.API.Controllers
{
    [Route("api/v1/cart")]
    [Authorize]
    public class CartController : BaseController
    {
        private readonly IAspNetUser _user;
        private readonly ICustomerCartRepository _customerCartRepository;

        public CartController(IAspNetUser user, 
                              ICustomerCartRepository customerCartRepository)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _customerCartRepository = customerCartRepository ?? throw new ArgumentNullException(nameof(customerCartRepository));
        }

        [HttpGet]
        public async Task<CustomerCart> GetCart()
        {
            return await GetCustomerCart() ?? new CustomerCart();
        }

        [HttpPost]
        public async Task<IActionResult> AddCartItem(CartItem item)
        {
            var cart = await GetCustomerCart();

            if (cart is null)
            {
                HandleNewCart(item);
            }
            else
            {
                HandleExistingCart(cart, item);
            }

            var result = await _customerCartRepository.UnitOfWork.CommitAsync();

            if (result) return CustomResponse();

            AddError("There was an error persisting the data.");

            return CustomResponse();
        }

        [HttpPut("{productId:guid}")]
        public async Task<IActionResult> EditCartItem(Guid productId, [FromBody] CartItem item)
        {
            return CustomResponse();
        }

        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> RemoveCartItem(Guid productId)
        {
            return CustomResponse();
        }

        private async Task<CustomerCart> GetCustomerCart()
        {
            var cart = await _customerCartRepository.GetCustomerCart(_user.GetUserId());

            return cart;
        }

        private void HandleNewCart(CartItem item)
        {
            var cart = new CustomerCart(_user.GetUserId());

            cart.AddItem(item);

            _customerCartRepository.AddCustomerCart(cart);
        }

        private void HandleExistingCart(CustomerCart cart, CartItem item)
        {
            var existingProduct = cart.ProductExistsInCart(item);

            cart.AddItem(item);

            if (existingProduct)
            {
                _customerCartRepository.UpdateCartItem(cart.GetProductById(item.ProductId));   
            }
            else
            {
                _customerCartRepository.AddCartItem(item);
            }

            _customerCartRepository.UpdateCustomerCart(cart);
        }
    }
}