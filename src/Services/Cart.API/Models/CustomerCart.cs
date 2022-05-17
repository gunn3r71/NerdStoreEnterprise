using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;
using NerdStoreEnterprise.Services.Cart.API.Validations;

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
        public ValidationResult ValidationResult { get; private set; }

        internal void AddItem(CartItem item)
        {
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
            Items.Remove(GetProductById(item.ProductId));
            CalculateCartValue();
        }
        
        internal bool ProductExistsInCart(CartItem item) =>
            Items.Any(p => p.ProductId.Equals(item.ProductId));

        internal CartItem GetProductById(Guid productId) => Items.SingleOrDefault(x => x.ProductId.Equals(productId));

        internal bool IsValid()
        {
            var errors = Items.SelectMany(x => new CartItemValidator().Validate(x).Errors).ToList();
            errors.AddRange(new CustomerCartValidator().Validate(this).Errors);

            ValidationResult = new ValidationResult(errors);

            return ValidationResult.IsValid;
        }

        private void UpdateItem(CartItem item)
        {
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