using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Services
{
	public interface IPhotoService
	{
		Task<Guid> Upload(string title, string url, Guid userId);
		Task<PhotoDto> GetById(Guid id);
		Task<IEnumerable<PhotoDto>> GetAll();
		Task<bool> IsPhotoBelongToUser(Guid userId, Guid photoId);
		Task<IEnumerable<PhotoDto>> GetAllUserPhotos(Guid userId);
		Task<IEnumerable<PhotoDto>> GetByTags(string tags);
		Task UpdateInfo(PhotoDto photoToUpdate, string newTitle);
		Task Delete(Guid id);
	}
}
