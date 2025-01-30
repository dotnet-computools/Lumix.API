
using Lumix.API.Contracts.Request.AuthRequest;
using Lumix.API.Contracts.Response;
using Lumix.API.Extensions;
using Lumix.Application.Auth;
using Lumix.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lumix.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
     
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                await _authService.Register(request.UserName, request.Email, request.Password);
                var registerResponse = new RegisterResponse
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    Password = request.Password
                };
                return Ok(registerResponse);
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
                var (accessToken, refreshToken) = await _authService.Login(request.Email, request.Password);
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                };
        
               Response.Cookies.Append("accessToken", accessToken, cookieOptions);
               Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
                var loginResponse = new LoginResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };

                return Ok(loginResponse);
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
                var newAccessToken = await _authService.RefreshToken(request.RefreshToken);
                return Ok(new { accessToken = newAccessToken });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-current-user")]
        public IActionResult GetProfile()
        {
            var userId = HttpContext.GetUserId();
        
            if (!userId.HasValue)
            {
                return Unauthorized();
            }

            return Ok(new { UserId = userId.Value });
        }
        [Authorize]
        [HttpPost("logout")]
        public IActionResult LogoutUser()
        {
            if (HttpContext.Request.Cookies.ContainsKey("accessToken"))
            {
                HttpContext.Response.Cookies.Delete("accessToken");
            }

            return Ok("User has been logged out successfully.");
        }
    }
}