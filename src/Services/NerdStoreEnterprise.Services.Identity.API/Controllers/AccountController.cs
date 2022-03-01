using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NerdStoreEnterprise.Services.Identity.API.Models;
using System.Threading.Tasks;

namespace NerdStoreEnterprise.Services.Identity.API.Controllers
{
    [Route("api/v1/account")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, 
                                 UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterViewModel user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(x => x.Errors.Select(e => e.ErrorMessage)));

            var applicationUser = new IdentityUser
            {
                UserName = user.Username,
                Email = user.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(applicationUser, user.Password);

            if (!result.Succeeded) return BadRequest();

            await _signInManager.SignInAsync(applicationUser, false);

            return Ok();
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginViewModel user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(x => x.Errors.Select(e => e.ErrorMessage)));

            var result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, false, true);

            if (!result.Succeeded) return BadRequest();

            return Ok();
        }
    }
}
