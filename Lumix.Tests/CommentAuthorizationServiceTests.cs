using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lumix.Application.Services;
using Lumix.Core.DTOs;

namespace Lumix.Tests
{
    public class CommentAuthorizationServiceTests
    {
        [Fact]
        public void AuthorOfComment_CanDelete_ReturnsTrue()
        {
            var userId = Guid.NewGuid();

            var comment = new CommentDto
            {
                Author = new UserPreviewDto { Id = userId }
            };

            var photo = new PhotoDto
            {
                UserId = Guid.NewGuid()
            };

            var sut = new CommentAuthorizationService();

            var result = sut.CanUserDeleteComment(comment, photo, userId);

            Assert.True(result);
        }

        [Fact]
        public void PhotoOwner_CanDelete_ReturnsTrue()
        {
            var userId = Guid.NewGuid();

            var comment = new CommentDto
            {
                Author = new UserPreviewDto { Id = Guid.NewGuid() }
            };

            var photo = new PhotoDto
            {
                UserId = userId
            };

            var sut = new CommentAuthorizationService();

            var result = sut.CanUserDeleteComment(comment, photo, userId);

            Assert.True(result);
        }

        [Fact]
        public void Stranger_CannotDelete_ReturnsFalse()
        {
            var userId = Guid.NewGuid();

            var comment = new CommentDto
            {
                Author = new UserPreviewDto { Id = Guid.NewGuid() }
            };

            var photo = new PhotoDto
            {
                UserId = Guid.NewGuid()
            };

            var sut = new CommentAuthorizationService();

            var result = sut.CanUserDeleteComment(comment, photo, userId);

            Assert.False(result);
        }


        [Fact]
        public void NullComment_ThrowsArgumentNullException()
        {
            var sut = new CommentAuthorizationService();

            Assert.Throws<ArgumentNullException>(() =>
                sut.CanUserDeleteComment(null!, new PhotoDto(), Guid.NewGuid()));
        }

        [Fact]
        public void NullPhoto_ThrowsArgumentNullException()
        {
            var sut = new CommentAuthorizationService();

            Assert.Throws<ArgumentNullException>(() =>
                sut.CanUserDeleteComment(new CommentDto(), null!, Guid.NewGuid()));
        }

    }
}
