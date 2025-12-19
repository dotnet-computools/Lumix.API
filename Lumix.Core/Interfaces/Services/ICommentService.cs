using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Services
{
	public interface ICommentService
	{
		Task<CommentDto> AddComment(Guid userId, Guid photoId, string commentText, Guid? parentId);
		Task<IEnumerable<CommentDto>> GetByPhotoId(Guid photoId);
        Task DeleteById(Guid commentId, Guid photoId, Guid currentUserId);
    }
}
