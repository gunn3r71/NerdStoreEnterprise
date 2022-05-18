using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using NerdStoreEnterprise.WebApp.Mvc.Services;

namespace NerdStoreEnterprise.WebApp.Mvc.Controllers
{
    [Route("cart")]
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("item/add")]
        public async Task<IActionResult> AddCartItem()
        {
            return RedirectToAction("Index");
        }

        [HttpPost("add/update")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, int amount)
        {
            return RedirectToAction("Index");
        }

        [HttpPost("add/renove")]
        public async Task<IActionResult> RemoveCartItem(Guid productId)
        {
            return RedirectToAction("Index");
        }
    }
}
