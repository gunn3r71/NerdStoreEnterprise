using System;

namespace NerdStoreEnterprise.WebApp.Mvc.Models.Cart
{
    public class CartItemViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}