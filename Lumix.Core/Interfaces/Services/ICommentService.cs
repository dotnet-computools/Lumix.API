using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Services
{
	public interface ICommentService
	{
		Task AddComment(Guid userId, Guid photoId, string commentText);
		Task<IEnumerable<CommentDto>?> GetByPhotoId(Guid photoId);
	}
}
