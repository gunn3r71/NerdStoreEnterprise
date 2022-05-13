using System;

namespace NerdStoreEnterprise.WebApp.Mvc.Models.Products
{
    public record ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Image { get; set; }
        public int QuantityInStock { get; set; }
    }
}