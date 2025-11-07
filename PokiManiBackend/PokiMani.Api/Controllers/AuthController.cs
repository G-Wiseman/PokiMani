using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PokiMani.Core.DTOs;
using PokiMani.Core.Interfaces.IServices;
using PokiMani.Infrastructure.Data;

namespace PokiMani.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManagerService _authmanager;

        public AuthController(
            IAuthManagerService authmanager)
        {
            _authmanager = authmanager;
        }

        [HttpPost("token")]
        public async Task<IActionResult> LoginWithPassword(SessionDto dto)
        {

            var authResult = await _authmanager.LoginWithPasswordAsync(dto.UserName, dto.Password);
            if (authResult == null){
                return Unauthorized();
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // only over HTTPS
                SameSite = SameSiteMode.Strict, // or Lax if needed
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("refreshToken", authResult.Value.RefreshToken, cookieOptions);

            return Ok(new
            {
                AccessToken = authResult.Value.Jwt,
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshJWT()
        {
            Request.Cookies.TryGetValue("refreshToken", out var oldRefreshToken);

            var authResult = await _authmanager.LoginWithRefreshTokenAsync(oldRefreshToken);
            if (!authResult.Succeeded) {
                return Unauthorized();
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // only over HTTPS
                SameSite = SameSiteMode.Strict, // or Lax if needed
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("refreshToken", authResult.Value.RefreshToken, cookieOptions);

            return Ok(new
            {
                AccessToken = authResult.Value.Jwt,
            });
        }

        [HttpPost("user")]
        public async Task<IActionResult> AddUser(AddUserDto dto)
        {
            var authResult = await _authmanager.RegisterNewUser(dto.Name, dto.Password, dto.Email);
            if (!authResult.Succeeded) { return BadRequest( new {Error = authResult.Error}); }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict, 
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("refreshToken", authResult!.Value!.RefreshToken, cookieOptions);

            return Ok(new
            {
                AccessToken = authResult!.Value!.Jwt,
            });
        }
    }
}
