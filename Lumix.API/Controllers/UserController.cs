using Lumix.API.Contracts.Request.AuthRequest;
using Lumix.API.Contracts.Response;
using Lumix.API.Extensions;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lumix.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpPut("username")]
    public async Task<ActionResult<UpdateUserRequest>> UpdateUsername([FromBody] UpdateUserRequest request)
    {
        var userId = HttpContext.GetUserId();
    
        if (!userId.HasValue)
        {
            return Unauthorized();
        }

        try
        {
            var updatedUser  = await _userService.UpdateUsernameAsync(userId.Value, request.Username);
            
            var response = new UpdateUserRequest()
            {
                Username = updatedUser.Username 
            };

            return Ok(response); 
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [Authorize]
    [HttpGet("profile")]
    public async Task<ActionResult<UserProfileDto>> GetProfile()
    {
        var userId = HttpContext.GetUserId();
        if (!userId.HasValue)
            return Unauthorized();
        var profile = await _userService.GetProfileAsync(userId.Value);
        
        return Ok(profile);
    }

    [Authorize]
    [HttpGet("me")]
    public ActionResult<Guid> GetCurrentUserId()
    {
        var userId = HttpContext.GetUserId();
        if (!userId.HasValue)
            return Unauthorized();
        
        return Ok(userId.Value);
    }
}