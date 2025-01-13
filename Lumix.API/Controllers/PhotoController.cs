using Lumix.API.Contracts.Requests.Photo;
using Lumix.API.Extensions;
using Lumix.Application.PhotoUpload;
using Lumix.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lumix.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PhotoController : Controller
	{
		private readonly IPhotoService _photoService;
		private readonly ITagService _tagService;
		private readonly IPhotoTagService _photoTagService;
		private readonly IFileStorageService _storageService;

		public PhotoController(IPhotoService photoService, ITagService service, IPhotoTagService photoTagService, IFileStorageService storageService)
		{
			_photoService = photoService;
			_tagService = service;
			_photoTagService = photoTagService;
			_storageService = storageService;
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

				var photoS3Url = await _storageService.UploadFileToStorage(uploadRequest.PhotoFile, userId);
				await _storageService.UploadThumbnailToStorage(uploadRequest.PhotoFile, userId);
				var newPhotoId = await _photoService.Upload(uploadRequest.Title, photoS3Url, userId);

				await _tagService.CheckAndAddNewTags(uploadRequest.Tags ?? Enumerable.Empty<string>());
				var tags = await _tagService.GetAllTagsFromStrings(uploadRequest.Tags ?? Enumerable.Empty<string>());

				await _photoTagService.AddNewRange(tags, newPhotoId);

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

				var photoToDelete = await _photoService.GetById(id);

				await _storageService.DeleteFileFromStorage(photoToDelete.Url, userId);
				await _storageService.DeleteThumbnailFromStorage(photoToDelete.Url, userId);

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
				await _photoService.UpdateInfo(photo, updateRequest.Title);

				await _tagService.CheckAndAddNewTags(updateRequest.Tags ?? Enumerable.Empty<string>());
				var oldTags = await _tagService.GetAllTagsFromStrings(updateRequest.Tags ?? Enumerable.Empty<string>());

				await _photoTagService.RemoveAllByPhotoId(photo.Id);
				var newPhotoTags = _photoTagService.AddNewRange(oldTags, id);

				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("search")]
		public async Task<IActionResult> SearchByTags([FromQuery] IEnumerable<string> tagsNames)
		{
			try
			{
				var userId = HttpContext.GetUserId() ?? Guid.Empty;
				if (userId == Guid.Empty)
				{
					return Unauthorized();
				}

				var tags = await _tagService.GetAllTagsFromStrings(tagsNames);
				var photosId = await _photoTagService.GetPhotosIdByTagsId(tags.Select(t => t.Id));
				var photos = await _photoService.GetByIds(photosId);

				return Ok(photos);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
