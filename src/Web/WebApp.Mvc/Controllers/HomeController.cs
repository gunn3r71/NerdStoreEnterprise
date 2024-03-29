﻿using System;
using System.Net;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NerdStoreEnterprise.WebApp.Mvc.Models.Errors;

namespace NerdStoreEnterprise.WebApp.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Catalog");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("error/{code:length(3,3):int}")]
        public IActionResult Error(int code)
        {
            var model = new ResponseResult
            {
                Title = "Oops, something went wrong..",
                Status = code,
                Description = (HttpStatusCode) code switch
                {
                    HttpStatusCode.InternalServerError =>
                        "We are unable to fulfill your request at the moment, please try again later.",
                    HttpStatusCode.Forbidden => "You are not allowed to do this.",
                    HttpStatusCode.NotFound => "Page not found.",
                    _ => "Page not found."
                }
            };

            return View("Error", model);
        }

        [AllowAnonymous]
        [Route("system-unavailable")]
        public IActionResult SystemUnavailable()
        {
            var model = new ResponseResult
            {
                Title = "System Unavailable",
                Description = "The system is temporarily unavailable, this can happen due to user overload.",
                Status = (int) HttpStatusCode.InternalServerError
            };

            return View("Error", model);
        }
    }
}