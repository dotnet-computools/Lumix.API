using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Repositories
{
	public interface ICommentsRepository
	{
		Task<CommentDto> AddAsync(CommentDto comment);
		Task<IEnumerable<CommentDto>?> GetByPhotoId(Guid photoId);
	}
}
