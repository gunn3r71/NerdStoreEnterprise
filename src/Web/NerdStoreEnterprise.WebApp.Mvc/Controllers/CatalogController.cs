using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.WebApp.Mvc.Controllers
{
    public class CatalogController : BaseController
    {
        [HttpGet]
        [Route("")]
        [Route("catalog")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet("catalog/product/details/{id:guid}")]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            return View();
        }
    }
}
