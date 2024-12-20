using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Services
{
	public interface IPhotoService
	{
		Task Upload(string title, string tags, string url, Guid userId);
		Task<PhotoDto> GetById(Guid id);
		Task<IEnumerable<PhotoDto>> GetAllUserPhotos(Guid userId);
	}
}
