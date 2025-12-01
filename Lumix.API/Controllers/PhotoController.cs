using Lumix.API.Contracts.Requests.Photo;
using Lumix.API.Contracts.Response.Photo;
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
		private readonly IUserService _userService;
        private readonly IFileStorageService _storageService;

		public PhotoController(IPhotoService photoService, ITagService service, IPhotoTagService photoTagService, IFileStorageService storageService, IUserService userService)
		{
			_photoService = photoService;
			_tagService = service;
			_photoTagService = photoTagService;
			_storageService = storageService;
			_userService = userService;
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

				var photoId = Guid.NewGuid();
				var photoS3Url = await _storageService.UploadFileToStorage(uploadRequest.PhotoFile, photoId, userId);
				await _storageService.UploadThumbnailToStorage(uploadRequest.PhotoFile, photoId, userId);
				var newPhotoId = await _photoService.Upload(uploadRequest.Title, photoS3Url, photoId, userId, uploadRequest.IsAvatar);

				if (uploadRequest.IsAvatar)
				{
					await _userService.UpdateProfilePictureAsync(userId, photoS3Url);
				}
				else
				{
                    await _tagService.CheckAndAddNewTags(uploadRequest.Tags ?? Enumerable.Empty<string>());
                    var tags = await _tagService.GetAllTagsFromStrings(uploadRequest.Tags ?? Enumerable.Empty<string>());

                    await _photoTagService.AddNewRange(tags, newPhotoId);
                }

				var response = new PhotoUploadResponse
				{
					PhotoId = newPhotoId,
					PhotoUrl = photoS3Url
				};

				return Ok(response);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("feed/{pageNumber:int}")]
		public async Task<IActionResult> GetFeedByTags([FromQuery] IEnumerable<string> tags, int pageNumber = 1)
		{
			try
			{
				var userId = HttpContext.GetUserId() ?? Guid.Empty;
				if (userId == Guid.Empty)
				{
					return Unauthorized();
				}

				var tagsList = await _tagService.GetAllTagsFromStrings(tags);
				var photosId = await _photoTagService.GetPhotosIdByTagsId(tagsList.Select(t => t.Id));
				var photos = await _photoService.GetByIds(photosId);
				var pagedPhotos = _photoService.GetFromCollectionByPage(photos, pageNumber);

				return Ok(pagedPhotos);
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
	}
}