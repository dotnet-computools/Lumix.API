using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Repositories
{
	public interface ILikesRepository
	{
		Task Add(LikeDto like);
		Task<LikeDto?> GetByUserPhotoId(Guid userId, Guid photoId);
		Task DeleteByUserPhotoId(Guid userId, Guid photoId);
	}
}
