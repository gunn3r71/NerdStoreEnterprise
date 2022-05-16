using System;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Messages.IntegrationEvents;
using NerdStoreEnterprise.Services.Cart.API.Validations;

namespace NerdStoreEnterprise.Services.Cart.API.Models
{
    public class CartItem : Entity
    {
        public CartItem(Guid productId, string name, int amount, decimal price, string image)
        {
            ProductId = productId;
            Name = name;
            Amount = amount;
            Price = price;
            Image = image;
        }

        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public int Amount { get; private set; }
        public decimal Price { get; private set; }
        public string Image { get; private set; }
        public Guid CartId { get; private set; }
        public CustomerCart Cart { get; private set; }

        internal void SetCart(Guid cartId)
        {
            if (cartId.Equals(Guid.Empty)) throw new ArgumentOutOfRangeException(nameof(cartId), "Cart Id cannot be empty.");
            
            CartId = cartId;
        }

        internal decimal CalculateValue() => Price * Amount;

        internal void AddUnits(int amount) => Amount += amount;

        internal void UpdateUnits(int amount) => Amount = amount;
        
        internal bool IsValid() => new CartItemValidator().Validate(this).IsValid;
    }
}