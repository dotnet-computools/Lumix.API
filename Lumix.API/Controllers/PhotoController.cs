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
		public async Task<IActionResult> Upload(UploadRequest uploadRequest)
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
			catch (Exception)
			{
				return BadRequest();
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
			catch (Exception)
			{
				return BadRequest();
			}
		}

		[HttpDelete("{id}")]
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
			catch (Exception)
			{

				throw;
			}
		}
	}
}
