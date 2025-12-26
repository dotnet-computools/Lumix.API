using Lumix.Application.Services;
using Lumix.Core.DTOs;

namespace Lumix.Tests
{
    public class CommentAuthorizationServiceTests
    {

        [Theory]
        [InlineData(true, false)] // Author of comment
        [InlineData(false, true)] // Owner of photo
        public void AuthorOfCommentOrPhotoOwner_CanDelete_ReturnsTrue(bool isAuthor, bool isPhotoOwner)
        {
            var userId = Guid.NewGuid();
            var comment = new CommentDto
            {
                Author = new UserPreviewDto { Id = isAuthor ? userId : Guid.NewGuid() }
            };
            var photo = new PhotoDto
            {
                UserId = isPhotoOwner ? userId : Guid.NewGuid()
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
