using System;
using NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects;

namespace NerdStoreEnterprise.Services.Catalog.API.Models
{
    public class Product : Entity, IAggregateRoot
    {
        protected Product()
        {
        }

        public Product(string name,
            string description,
            bool status,
            decimal price,
            DateTime createdAt,
            string image,
            int quantityInStock) : this()
        {
            Name = name;
            Description = description;
            Status = status;
            Price = price;
            CreatedAt = createdAt;
            Image = image;
            QuantityInStock = quantityInStock;
        }

        public string Name { get; }
        public string Description { get; }
        public bool Status { get; }
        public decimal Price { get; }
        public DateTime CreatedAt { get; }
        public string Image { get; }
        public int QuantityInStock { get; }
    }
}