
using Lumix.API.Contracts.Requests;
using Lumix.Core.Interfaces.Services;
using Lumix.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lumix.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                await _userService.Register(request.UserName, request.Email, request.Password);
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var tokens = await _userService.Login(request.Email, request.Password);
                Response.Cookies.Append("AccessToken", tokens.AccessToken,
                    new CookieOptions { HttpOnly = true, Secure = true });
                return Ok(new { AccessToken = tokens.AccessToken, RefreshToken = tokens.RefreshToken });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var newAccessToken = await _userService.RefreshToken(request.RefreshToken);
                Response.Cookies.Append("AccessToken", newAccessToken,
                    new CookieOptions { HttpOnly = true, Secure = true });

                return Ok(new { AccessToken = newAccessToken });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}