using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Core.Interfaces.Services;

namespace Lumix.Application.Services
{
	public class CommentService : ICommentService
	{
		private readonly ICommentsRepository _commentsRepository;

		public CommentService(ICommentsRepository commentsRepository)
		{
			_commentsRepository = commentsRepository;
		}

		public async Task AddComment(Guid userId, Guid photoId, string commentText)
		{
			var comment = new CommentDto
			{
				Id = Guid.NewGuid(),
				UserId = userId,
				PhotoId = photoId,
				Text = commentText,
				CreatedAt = DateTime.UtcNow
			};


            await _commentsRepository.Add(comment);
		}

		public async Task<IEnumerable<CommentDto>?> GetByPhotoId(Guid photoId)
		{
			return await _commentsRepository.GetByPhotoId(photoId);
		}
	}
}
