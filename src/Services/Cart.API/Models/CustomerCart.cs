using System;
using System.Collections.Generic;
using System.Linq;
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

        internal bool ProductExistsInCart(CartItem item) =>
            Items.Any(p => p.ProductId.Equals(item.ProductId));

        internal CartItem GetProductById(Guid productId) => Items.SingleOrDefault(x => x.ProductId.Equals(productId));

        private void CalculateCartValue()
        {
            Total = Items.Sum(item => item.CalculateValue());
        }
    }
}