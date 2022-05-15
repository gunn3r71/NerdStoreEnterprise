using System;
using System.Collections.Generic;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;

namespace NerdStoreEnterprise.Services.Cart.API.Models
{
    public class CustomerCart : Entity
    {
        public CustomerCart(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; private set; }
        public decimal Total { get; private set; }
        public List<CartItem> Items { get; private set; } = new();
    }
}