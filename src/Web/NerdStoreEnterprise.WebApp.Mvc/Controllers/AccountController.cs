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
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterViewModel register)
        {
            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
    }
}
