﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.Identity;
using NerdStoreEnterprise.Services.Identity.API.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using NerdStoreEnterprise.BuildingBlocks.Services.Core.Controllers;

namespace NerdStoreEnterprise.Services.Identity.API.Controllers
{
    [Route("api/v1/account")]
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenSettings _tokenSettings;

        public AccountController(SignInManager<IdentityUser> signInManager,
                                 UserManager<IdentityUser> userManager,
                                 IOptions<TokenSettings> tokenSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenSettings = tokenSettings.Value;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterViewModel user)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var applicationUser = new IdentityUser
            {
                UserName = user.Username,
                Email = user.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(applicationUser, user.Password);

            if (result.Succeeded) return CustomResponse(await GetTokenAsync(user.Username));

            AddError(result.Errors.Select(x => x.Description));

            return CustomResponse();
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginViewModel user)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, false, true);

            if (result.Succeeded) return CustomResponse(await GetTokenAsync(user.Username));

            if (result.IsLockedOut)
            {
                AddError("User stuck, too many invalid attempts.");
                return CustomResponse();
            }

            AddError("incorrect username or password.");
            return CustomResponse();
        }

        private async Task<TokenViewModel> GetTokenAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            var userClaims = await GetUserClaims(user);

            return new TokenViewModel
            {
                AccessToken = GenerateToken(userClaims),
                ExpiresIn = TimeSpan.FromHours(_tokenSettings.ExpiresAt).TotalSeconds,
                UserTokenInformation = new UserTokenInformationViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Claims = userClaims.Claims.Select(x => new UserClaimViewModel { Type = x.Type, Value = x.Value })
                }
            };
        }

        private string GenerateToken(ClaimsIdentity claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenSettings.Issuer,
                Audience = _tokenSettings.Audience,
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(_tokenSettings.ExpiresAt),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private async Task<ClaimsIdentity> GetUserClaims(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.Now).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.Now).ToString(),
                ClaimValueTypes.Integer64));

            foreach (var role in roles) claims.Add(new Claim("role", role));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private static long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}