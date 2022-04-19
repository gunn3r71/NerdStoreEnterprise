using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        [Route("catalog")]
        public async Task<IActionResult> Index() => 
            View(await _catalogService.GetAll());

        [HttpGet("catalog/product/details/{id:guid}")]
        public async Task<IActionResult> ProductDetails(Guid id) => 
            View(await _catalogService.GetById(id));
    }
}