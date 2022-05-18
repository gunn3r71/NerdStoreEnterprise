using System.Collections.Generic;

namespace NerdStoreEnterprise.WebApp.Mvc.Models.Cart
{
    public class CartViewModel
    {
        public decimal Total { get; set; }
        public List<CartItemViewModel> Items { get; set; } = new();
    }
}