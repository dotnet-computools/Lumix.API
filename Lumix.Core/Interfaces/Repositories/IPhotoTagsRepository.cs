using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Repositories
{
	public interface IPhotoTagsRepository
	{
		Task Add(PhotoTagDto photoTag);
		Task AddRange(IEnumerable<PhotoTagDto> photoTags);
		Task<IEnumerable<PhotoTagDto>> GetByPhotoId(Guid photoId);
		Task DeleteAllByPhotoId(Guid photoId);
	}
}
