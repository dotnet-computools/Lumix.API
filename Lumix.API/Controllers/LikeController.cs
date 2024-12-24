using Lumix.API.Extensions;
using Lumix.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lumix.API.Controllers
{
	[ApiController]
	[Route("api/Photo")]
	public class LikeController : Controller
	{
		private readonly ILikeService _likeService;

		public LikeController(ILikeService likeService)
		{
			_likeService = likeService;
		}

		[HttpPost("{id:guid}/like")]
		public async Task<IActionResult> Like(Guid id)
		{
			try
			{

				var userId = HttpContext.GetUserId() ?? Guid.Empty;
				if (userId == Guid.Empty)
				{
					return Unauthorized();
				}

				var isLiked = await _likeService.IsUserLikedPhoto(userId, id);
				if (isLiked)
				{
					await _likeService.Remove(userId, id);
					return Ok();
				}

				await _likeService.Like(userId, id);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id:guid}/is-liked")]
		public async Task<IActionResult> CheckIsLiked(Guid id)
		{
			try
			{
				var userId = HttpContext.GetUserId() ?? Guid.Empty;
				if (userId == Guid.Empty)
				{
					return Unauthorized();
				}

				var isLiked = await _likeService.IsUserLikedPhoto(userId, id);
				if (!isLiked)
				{
					return Ok(false);
				}
				return Ok(true);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
