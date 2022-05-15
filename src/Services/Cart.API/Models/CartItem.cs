using System;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;

namespace NerdStoreEnterprise.Services.Cart.API.Models
{
    public class CartItem : Entity
    {
        public CartItem(Guid productId, string name, int amount, decimal price, string image, Guid cartId)
        {
            ProductId = productId;
            Name = name;
            Amount = amount;
            Price = price;
            Image = image;
            CartId = cartId;
        }

        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public int Amount { get; private set; }
        public decimal Price { get; private set; }
        public string Image { get; private set; }
        public Guid CartId { get; private set; }
        public CustomerCart Cart { get; private set; }
    }
}