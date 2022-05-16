using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.Extensions.WebEncoders.Testing;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;

namespace NerdStoreEnterprise.Services.Cart.API.Models
{
    public class CustomerCart : Entity, IAggregateRoot
    {
        public CustomerCart()
        {
        }

        public CustomerCart(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; private set; }
        public decimal Total { get; private set; }
        public List<CartItem> Items { get; private set; } = new();

        internal void AddItem(CartItem item)
        {
            if (!item.IsValid()) return;

            item.SetCart(Id);

            if (ProductExistsInCart(item))
            {
                var existingItem = GetProductById(item.ProductId);
                existingItem.AddUnits(item.Amount);

                item = existingItem;

                Items.Remove(existingItem);
            }

            Items.Add(item);

            CalculateCartValue();
        }
        
        internal void UpdateUnits(CartItem item, int amount)
        {
            item.UpdateUnits(amount);
            UpdateItem(item);
        }

        internal void RemoveItem(CartItem item)
        {
            if (!item.IsValid()) return;

            var existingItem = GetProductById(item.ProductId);

            if (existingItem is null) throw new DomainException("The item isn't in the cart.");
            Items.Remove(existingItem);
            
            CalculateCartValue();
        }
        
        internal bool ProductExistsInCart(CartItem item) =>
            Items.Any(p => p.ProductId.Equals(item.ProductId));

        internal CartItem GetProductById(Guid productId) => Items.SingleOrDefault(x => x.ProductId.Equals(productId));

        private void UpdateItem(CartItem item)
        {
            if (!item.IsValid()) return;
            
            item.SetCart(Id);

            var existingItem = GetProductById(item.ProductId);

            Items.Remove(existingItem);
            Items.Add(item);
            
            CalculateCartValue();
        }
        
        private void CalculateCartValue()
        {
            Total = Items.Sum(item => item.CalculateValue());
        }
    }
}