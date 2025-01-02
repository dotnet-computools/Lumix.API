using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Services
{
	public interface IPhotoTagService
	{
		Task AddNew(Guid tagId, Guid photoId);
		Task AddNewRange(IEnumerable<TagDto> photoTags, Guid photoId);
		Task<IEnumerable<PhotoTagDto>> GetAllByPhotoId(Guid photoId);
		Task RemoveAllByPhotoId(Guid photoId);
	}
}
