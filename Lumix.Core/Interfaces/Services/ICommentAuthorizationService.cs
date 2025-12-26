using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Services
{
    public interface ICommentAuthorizationService
    {
        bool CanUserDeleteComment(CommentDto comment, PhotoDto photo, Guid currentUserId);
    }
}
