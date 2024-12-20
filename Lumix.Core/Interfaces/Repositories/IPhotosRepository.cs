using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Repositories
{
	public interface IPhotosRepository
	{
		Task Add(PhotoDto photo);
		Task<PhotoDto> GetById(Guid id);
		Task<PhotoDto> GetByUserAndPhotoId(Guid userId, Guid photoId);
		Task<IEnumerable<PhotoDto>> GetAllByUserId(Guid userId);
		Task Update(PhotoDto photo);
		Task DeleteById(Guid id);
	}
}
