using Lumix.API.Contracts.Requests;
using Lumix.API.Extensions;
using Lumix.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lumix.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CommentController : Controller
	{
		private readonly ICommentService _commentService;

		public CommentController(ICommentService commentService)
		{
			_commentService = commentService;
		}

		[HttpPost("{id:guid}")]
		public async Task<IActionResult> PostComment(Guid id, [FromBody] CommentRequest request)
		{
			try
			{
				var userId = HttpContext.GetUserId() ?? Guid.Empty;
				if (userId == Guid.Empty)
				{
					return Unauthorized();
				}

				var created = await _commentService.AddComment(userId, id, request.Text, request.ParentId);
				return Ok(created);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("all/{id:guid}")]
		public async Task<IActionResult> GetComments(Guid id)
		{
			try
			{
				var userId = HttpContext.GetUserId() ?? Guid.Empty;
				if (userId == Guid.Empty)
				{
					return Unauthorized();
				}

				var comments = await _commentService.GetByPhotoId(id);
				return Ok(comments);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
