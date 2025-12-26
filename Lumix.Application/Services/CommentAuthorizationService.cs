using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Services;

namespace Lumix.Application.Services
{
    public class CommentAuthorizationService : ICommentAuthorizationService
    {

        public bool CanUserDeleteComment(CommentDto comment, PhotoDto photo, Guid currentUserId)
        {
            if (comment == null) throw new ArgumentNullException(nameof(comment));
            if (photo == null) throw new ArgumentNullException(nameof(photo));
            if (comment.Author == null) throw new ArgumentNullException(nameof(comment.Author));

            return comment.Author.Id == currentUserId
                   || photo.UserId == currentUserId;
        }
    }
}
