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

		public async Task AddComment(Guid userId, Guid photoId, string commentText, Guid? parentId)
		{
			var comment = new CommentDto
			{
				Id = Guid.NewGuid(),
				UserId = userId,
				PhotoId = photoId,
				Text = commentText,
				CreatedAt = DateTime.UtcNow,
				ParentId = parentId
            };


            await _commentsRepository.Add(comment);
		}

		public async Task<IEnumerable<CommentDto>> GetByPhotoId(Guid photoId)
		{
			var flatDtos = await _commentsRepository.GetByPhotoId(photoId)
				?? Enumerable.Empty<CommentDto>();

            var tree = BuildTree(flatDtos.ToList());
			return tree;
		}

        private static List<CommentDto> BuildTree(List<CommentDto> flat)
        {
            var lookup = flat.ToDictionary(c => c.Id);
            var roots = new List<CommentDto>();

            foreach (var comment in flat)
            {
                if (comment.ParentId == null)
                {
                    roots.Add(comment);
                }
                else if (lookup.TryGetValue(comment.ParentId.Value, out var parent))
                {
                    parent.Children.Add(comment);
                }
            }

            return roots;
        }
    }
}
