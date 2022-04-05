using System;
using NerdStoreEnterprise.BuildingBlocks.Core.DomainObjects;

namespace NerdStoreEnterprise.Services.Catalog.API.Models
{
    public class Product : Entity
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

        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Status { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Image { get; private set; }
        public int QuantityInStock { get; private set; }
    }
}
