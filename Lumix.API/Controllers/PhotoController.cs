using Lumix.API.Contracts.Requests.Photo;
using Lumix.API.Extensions;
using Lumix.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lumix.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PhotoController : Controller
	{
		private readonly IPhotoService _photoService;

		public PhotoController(IPhotoService photoService)
		{
			_photoService = photoService;
		}

		[HttpPost("upload")]
		public async Task<IActionResult> Upload([FromForm] UploadRequest uploadRequest)
		{
			try
			{
				var userId = HttpContext.GetUserId() ?? Guid.Empty;
				if (userId == Guid.Empty)
				{
					return Unauthorized();
				}

				//s3 upload logic
				await _photoService.Upload(uploadRequest.Title, uploadRequest.Tags, url: "empty", userId);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("feed")]
		public async Task<IActionResult> GetFeed()
		{
			try
			{
				var userId = HttpContext.GetUserId() ?? Guid.Empty;
				if (userId == Guid.Empty)
				{
					return Unauthorized();
				}

				var photos = await _photoService.GetAll();
				return Ok(photos);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetFullSizeById(Guid id)
		{
			try
			{
				var userId = HttpContext.GetUserId() ?? Guid.Empty;
				if (userId == Guid.Empty)
				{
					return Unauthorized();
				}

				var photo = await _photoService.GetById(id);
				//s3 get logic

				return Ok(photo);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteById(Guid id)
		{
			try
			{
				var userId = HttpContext.GetUserId() ?? Guid.Empty;
				if (userId == Guid.Empty)
				{
					return Unauthorized();
				}

				var result = await _photoService.IsPhotoBelongToUser(userId, id);
				if (!result)
				{
					return Forbid();
				}

				await _photoService.Delete(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRequest updateRequest)
		{
			try
			{
				var userId = HttpContext.GetUserId() ?? Guid.Empty;
				if (userId == Guid.Empty)
				{
					return Unauthorized();
				}

				var result = await _photoService.IsPhotoBelongToUser(userId, id);
				if (!result)
				{
					return Forbid();
				}

				var photo = await _photoService.GetById(id);
				await _photoService.UpdateInfo(photo, updateRequest.Title, updateRequest.Tags);

				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
