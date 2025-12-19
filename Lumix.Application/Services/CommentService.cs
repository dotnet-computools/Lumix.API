using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Core.Interfaces.Services;

namespace Lumix.Application.Services
{
	public class CommentService : ICommentService
	{
		private readonly ICommentsRepository _commentsRepository;
		private readonly IPhotosRepository _photosRepository;

        public CommentService(ICommentsRepository commentsRepository, IPhotosRepository photosRepository)
		{
			_commentsRepository = commentsRepository;
			_photosRepository = photosRepository;
        }

		public async Task<CommentDto> AddComment(Guid userId, Guid photoId, string commentText, Guid? parentId)
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


            var created = await _commentsRepository.AddAsync(comment);
			return created;
		}

		public async Task<IEnumerable<CommentDto>> GetByPhotoId(Guid photoId)
		{
			var allComments = await _commentsRepository.GetByPhotoId(photoId)
				?? Enumerable.Empty<CommentDto>();

            var tree = BuildTree(allComments.ToList());
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

        public async Task DeleteById(Guid commentId, Guid photoId, Guid currentUserId)
        {
            var photo = await _photosRepository.GetById(photoId);
            var comment = await _commentsRepository.GetById(commentId);

            if (comment.Author.Id != currentUserId && photo.UserId != currentUserId)
                throw new UnauthorizedAccessException();

            await _commentsRepository.DeleteById(commentId);
        }
    }
}
