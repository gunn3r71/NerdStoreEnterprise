using System;
using System.Linq;
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
        private readonly IUser _user;
        private readonly ICustomerCartRepository _customerCartRepository;

        public CartController(IUser user, 
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

        [HttpPost("item")]
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

            if (!ValidateCart(cart)) return CustomResponse();

            await PersistData();
            
            return CustomResponse();
        }

        [HttpPut("item/{productId:guid}")]
        public async Task<IActionResult> EditCartItem(Guid productId, [FromBody] CartItem item)
        {
            var customerCart = await GetCustomerCart();
            var cartItem = await GetValidatedCartItem(productId, customerCart, item);

            if (cartItem is null) return CustomResponse();

            customerCart.UpdateUnits(cartItem, item.Amount);

            if (!ValidateCart(customerCart)) return CustomResponse();
            
            _customerCartRepository.UpdateCartItem(cartItem);
            _customerCartRepository.UpdateCustomerCart(customerCart);

            await PersistData();
            
            return CustomResponse();
        }

        [HttpDelete("item/{productId:guid}")]
        public async Task<IActionResult> RemoveCartItem(Guid productId)
        {
            var cart = await GetCustomerCart();

            var cartItem = await GetValidatedCartItem(productId, cart);

            if (cartItem is null) return CustomResponse();

            if (!ValidateCart(cart)) return CustomResponse();

            cart.RemoveItem(cartItem);
            
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

        private async Task<CartItem> GetValidatedCartItem(Guid productId, CustomerCart customerCart, CartItem item = null)
        {
            if (item is not null && !productId.Equals(item.ProductId))
            {
                AddError("The item does not match what was reported.");
                return null;
            }

            if (customerCart is null)
            {
                AddError("Cart not found.");
                return null;
            }

            var cartItem = await _customerCartRepository.GetCartItem(customerCart.Id, productId);

            if (cartItem is not null && !customerCart.ProductExistsInCart(cartItem)) return cartItem;

            AddError("The item isn't in the cart.");

            return null;
        }

        private async Task PersistData()
        {
            var success = await _customerCartRepository.UnitOfWork.CommitAsync();

            if (!success) AddError("There was an error persisting the data.");
        }

        private bool ValidateCart(CustomerCart cart)
        {
            if (cart.IsValid()) return true;

            AddError(cart.ValidationResult.Errors.Select(x => x.ErrorMessage));

            return false;
        }
    }
}