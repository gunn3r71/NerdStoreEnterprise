using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NerdStoreEnterprise.WebApp.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NerdStoreEnterprise.WebApp.Mvc.Models.Errors;

namespace NerdStoreEnterprise.WebApp.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

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
