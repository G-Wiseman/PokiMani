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
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManagerService _authmanager;

        public AuthController(
            IAuthManagerService authmanager)
        {
            _authmanager = authmanager;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDto))]
        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> LoginWithPassword(SessionDto dto)
        {
            var authResult = await _authmanager.LoginWithPasswordAsync(dto.UserName, dto.Password);
            if (authResult == null) {
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

            return Ok(new TokenDto
            {
                AccessToken = authResult.Value.Jwt,
            });
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(TokenDto))]
        [HttpPost("refresh")]
        public async Task<ActionResult<TokenDto>> RefreshJWT()
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

            return Ok(new TokenDto
            {
                AccessToken = authResult.Value.Jwt,
            });
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDto))]
        [HttpPost("register")]
        public async Task<ActionResult<TokenDto>> RegisterNewUser(AddUserDto dto)
        {
            var authResult = await _authmanager.RegisterNewUserAsync(dto.Name, dto.Password, dto.Email);
            if (!authResult.Succeeded) { return BadRequest( new {Error = authResult.Error}); }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict, 
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("refreshToken", authResult!.Value!.RefreshToken, cookieOptions);

            return Ok(new TokenDto
            {
                AccessToken = authResult!.Value!.Jwt,
            });
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Request.Cookies.TryGetValue("refreshToken", out var refreshToken);
            await _authmanager.LogoutRefreshSessionAsync(refreshToken);
            return NoContent();
        }
    }
}
