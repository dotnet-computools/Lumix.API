using Lumix.API.Extensions;
using Lumix.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lumix.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FollowController : ControllerBase
{
    private readonly IFollowService _followService;
    
    public FollowController(IFollowService followService)
    {
        _followService = followService;
    }
    
    
    [HttpPost("{id}/follow")]
    public async Task<IActionResult> FollowUser(Guid id)
    {
        var userId = HttpContext.GetUserId();

        if (!userId.HasValue)
        {
            return Unauthorized();
        }

        try
        {
            await _followService.FollowUser(userId.Value, id);
            return Ok("User followed successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}/is-following")]
    public async Task<IActionResult> IsFollowing(Guid id)
    {
        var userId = HttpContext.GetUserId();

        if (!userId.HasValue)
        {
            return Unauthorized();
        }

        try
        {
            var isFollowing = await _followService.IsFollowing(userId.Value, id);
            return Ok(new { isFollowing });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}/following")]
    public async Task<IActionResult> GetFollowing(Guid id)
    {
        var userId = HttpContext.GetUserId();

        if (!userId.HasValue)
        {
            return Unauthorized();
        }

        try
        {
            var followingIds = await _followService.GetFollowing(id);
            return Ok(followingIds); // Повертаємо лише ID
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}