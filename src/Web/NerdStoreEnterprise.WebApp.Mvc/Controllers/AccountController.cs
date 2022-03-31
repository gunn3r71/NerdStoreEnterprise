﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using NerdStoreEnterprise.WebApp.Mvc.Models.Users;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using NerdStoreEnterprise.WebApp.Mvc.Models.AuthenticationResponse;
using IAuthenticationService = NerdStoreEnterprise.WebApp.Mvc.Services.IAuthenticationService;

namespace NerdStoreEnterprise.WebApp.Mvc.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet("access-denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginViewModel login, string returnUrl = null)
        {
            if (!ModelState.IsValid) return View(login);
            
            var result = await _authenticationService.Login(login);

            if (HasErrors(result.ErrorDetails)) return View(login);

            if (string.IsNullOrWhiteSpace(returnUrl)) return RedirectToAction("Index", "Home");

            return LocalRedirect(returnUrl);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterViewModel register)
        {
            if (!ModelState.IsValid) return View(register);

            var result = await _authenticationService.Register(register);

            if (HasErrors(result.ErrorDetails)) return View(register);

            await ApplicationAuthenticationAsync(result);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }

        private async Task ApplicationAuthenticationAsync(TokenViewModel tokenModel)
        {
            var token = GetFormattedJwtSecurityToken(tokenModel.AccessToken);

            var userClaims = new List<Claim>();
            userClaims.Add(new Claim("JWT", tokenModel.AccessToken));
            userClaims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Console.WriteLine("Autenticado");
            }
        }

        private static JwtSecurityToken GetFormattedJwtSecurityToken(string token)
            => new JwtSecurityTokenHandler().ReadJwtToken(token);
    }
}