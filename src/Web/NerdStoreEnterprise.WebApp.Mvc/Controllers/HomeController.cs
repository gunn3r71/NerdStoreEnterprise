using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using NerdStoreEnterprise.WebApp.Mvc.Models.Errors;

namespace NerdStoreEnterprise.WebApp.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index() => RedirectToAction("Index", "Catalog");

        public IActionResult Privacy() => View();

        [AllowAnonymous]
        [Route("error/{code:length(3,3):int}")]
        public IActionResult Error(int code)
        {
            var model = new ErrorViewModel
            {
                Title = "Oops, something went wrong..",
                Status = code,
                Description = (HttpStatusCode) code switch
                {
                    HttpStatusCode.InternalServerError => "We are unable to fulfill your request at the moment, please try again later.",
                    HttpStatusCode.Forbidden => "You are not allowed to do this.",
                    HttpStatusCode.NotFound => "Page not found.",
                    _ => "Page not found."
                }
            };

            return View("Error", model);
        }
    }
}
