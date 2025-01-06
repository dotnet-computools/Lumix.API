using Lumix.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lumix.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TagController : Controller
	{
		private readonly ITagService _tagService;
		private readonly IPhotoTagService _photoTagService;

		public TagController(ITagService tagService, IPhotoTagService photoTagService)
		{
			_tagService = tagService;
			_photoTagService = photoTagService;
		}

		[HttpGet("{photoId:guid}/tags")]
		public async Task<IActionResult> GetTags(Guid photoId)
		{
			try
			{
				var photoTags = await _photoTagService.GetAllByPhotoId(photoId);
				var tags = _tagService.GetAllByPhotoTagsByInclude(photoTags);

				return Ok(tags);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
