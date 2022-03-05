using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStoreEnterprise.WebApp.Mvc.Models.Users;

namespace NerdStoreEnterprise.WebApp.Mvc.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
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
        public async Task<IActionResult> Login(UserLoginViewModel login)
        {
            if (!ModelState.IsValid) return View(login);

            //TODO -> call api login endpoint

            if (false) return View(login);


            return RedirectToAction("Index", "Home");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterViewModel register)
        {
            if (!ModelState.IsValid) return View(register);

            //TODO -> call api register endpoint
            
            if (false) return View(register);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            //TODO -> clean cookies

            return RedirectToAction("Index", "Home");
        }
    }
}