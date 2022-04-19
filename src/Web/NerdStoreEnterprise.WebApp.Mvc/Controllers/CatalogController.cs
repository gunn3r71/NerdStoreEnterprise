using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NerdStoreEnterprise.WebApp.Mvc.Services;

namespace NerdStoreEnterprise.WebApp.Mvc.Controllers
{
    public class CatalogController : BaseController
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("")]
        [Route("catalog")]
        public async Task<IActionResult> Index()
        {
            var products = await _catalogService.GetAll();
            return View(products);
        }

        [HttpGet("catalog/product/details/{id:guid}")]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            var product = await _catalogService.GetById(id);

            return View(product);
        }
    }
}