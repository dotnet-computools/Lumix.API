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
    
    
    [HttpPost("{userid}/follow")]
    public async Task<IActionResult> FollowUser(Guid userid)
    {
        var currentUserId = HttpContext.GetUserId();

        if (!currentUserId.HasValue)
        {
            return Unauthorized();
        }

        try
        {
            await _followService.FollowUser(currentUserId.Value, userid);
            return Ok("User followed successfully");
        }
        catch (InvalidOperationException ex) when (ex.Message == "You are already following this person.")
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{userid}/is-following")]
    public async Task<IActionResult> IsFollowing(Guid userid)
    {
        var currentUserId = HttpContext.GetUserId();

        if (!currentUserId.HasValue)
        {
            return Unauthorized();
        }

        try
        {
            var isFollowing = await _followService.IsFollowing(currentUserId.Value, userid);
            return Ok(new { isFollowing });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{userid}/following")]
    public async Task<IActionResult> GetFollowing(Guid userid)
    {
        var currentUserId = HttpContext.GetUserId();

        if (!currentUserId.HasValue)
        {
            return Unauthorized();
        }

        try
        {
            var followingIds = await _followService.GetFollowing(userid);
            return Ok(followingIds); // Повертаємо лише ID
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{userid}/unfollow")] 
    public async Task<IActionResult> UnfollowUser(Guid userid)
    {
        var currentUserId = HttpContext.GetUserId();

        if (!currentUserId.HasValue)
        {
            return Unauthorized();
        }

        try
        {
            await _followService.UnfollowUser(currentUserId.Value, userid);
            return Ok("User unfollowed successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("{userId}/followers")]
    public async Task<IActionResult> GetFollowers(Guid userId)
    {
        try
        {
            var followers = await _followService.GetFollowers(userId);
            return Ok(followers);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}