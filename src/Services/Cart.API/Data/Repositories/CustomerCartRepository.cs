using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Data;
using NerdStoreEnterprise.Services.Cart.API.Models;

namespace NerdStoreEnterprise.Services.Cart.API.Data.Repositories
{
    public class CustomerCartRepository : ICustomerCartRepository
    {
        private readonly CartDbContext _context;

        public CustomerCartRepository(CartDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<CustomerCart> GetCustomerCart(Guid customerId)
        {
            return await _context.CustomerCarts
                    .Include(x => x.Items)
                    .FirstOrDefaultAsync(x => x.CustomerId.Equals(customerId));
        }

        public void AddCustomerCart(CustomerCart cart)
        {
            _context.CustomerCarts.Add(cart);
        }

        public void UpdateCustomerCart(CustomerCart cart)
        {
            _context.CustomerCarts.Update(cart);
        }

        public void AddCartItem(CartItem item)
        {
            _context.CartItems.Add(item);
        }

        public void UpdateCartItem(CartItem item)
        {
            _context.CartItems.Update(item);
        }

        public void Dispose() => _context?.Dispose();
    }
}