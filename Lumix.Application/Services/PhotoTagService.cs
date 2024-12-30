using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Core.Interfaces.Services;

namespace Lumix.Application.Services
{
	public class PhotoTagService : IPhotoTagService
	{
		private readonly IPhotoTagsRepository _photoTagRepository;

		public PhotoTagService(IPhotoTagsRepository photoTagsRepository)
		{
			_photoTagRepository = photoTagsRepository;
		}

		public async Task AddNew(Guid tagId, Guid photoId)
		{
			var newPhotoTag = PhotoTagDto.Create(
				Guid.NewGuid(),
				tagId,
				photoId,
				DateTime.UtcNow);

			await _photoTagRepository.Add(newPhotoTag);
		}

		public async Task AddNewRange(IEnumerable<TagDto> photoTags, Guid photoId)
		{
			var newPhotoTagsList = new List<PhotoTagDto>();

			foreach (var photoTag in photoTags)
			{
				var newPhotoTag = PhotoTagDto.Create(
					Guid.NewGuid(),
					photoTag.Id,
					photoId,
					DateTime.UtcNow);

				newPhotoTagsList.Add(newPhotoTag);
			}

			await _photoTagRepository.AddRange(newPhotoTagsList);
		}
	}
}
