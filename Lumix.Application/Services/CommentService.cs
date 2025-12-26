using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Core.Interfaces.Services;

namespace Lumix.Application.Services
{
	public class CommentService : ICommentService
	{
		private readonly ICommentsRepository _commentsRepository;
		private readonly IPhotosRepository _photosRepository;
        private readonly ICommentAuthorizationService _commentAuthorizationService;

        public CommentService(ICommentsRepository commentsRepository, IPhotosRepository photosRepository, ICommentAuthorizationService commentAuthorizationService)
		{
			_commentsRepository = commentsRepository;
			_photosRepository = photosRepository;
            _commentAuthorizationService = commentAuthorizationService;
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
            var comment = await _commentsRepository.GetById(commentId);
            var photo = await _photosRepository.GetById(photoId);
            if (_commentAuthorizationService.CanUserDeleteComment(comment, photo, currentUserId))
            {
                await _commentsRepository.DeleteById(commentId);
                return;
            } 
            
            throw new UnauthorizedAccessException();
        }
    }
}
