namespace Lumix.Core.Models;

public class UserDto
    {
        private readonly List<PhotoDto> _photos = [];
        private readonly List<LikeDto> _likes = [];
        private readonly List<CommentDto> _comments = [];
        private readonly List<FollowDto> _followers = [];
        private readonly List<FollowDto> _following = [];
        private readonly List<RefreshTokenDto> _refreshTokens = [];

        private UserDto(
            Guid id,
            string username,
            string email,
            string passwordHash,
            string? profilePictureUrl = null,
            string? bio = null)
        {
            Id = id;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            ProfilePictureUrl = profilePictureUrl;
            CreatedAt = DateTime.UtcNow;
            Bio = bio;
        }

        public Guid Id { get; }
        public string Username { get; } = string.Empty;
        public string Email { get; } = string.Empty;
        public string PasswordHash { get; } = string.Empty;
        public string? ProfilePictureUrl { get; }
        public DateTime CreatedAt { get; }
        public string? Bio { get; }

        public IReadOnlyList<PhotoDto> Photos => _photos;
        public IReadOnlyList<LikeDto> Likes => _likes;
        public IReadOnlyList<CommentDto> Comments => _comments;
        public IReadOnlyList<FollowDto> Followers => _followers;
        public IReadOnlyList<FollowDto> Following => _following;
        public IReadOnlyList<RefreshTokenDto> RefreshTokens => _refreshTokens;

        public static UserDto Create(
            Guid id,
            string username,
            string email,
            string passwordHash,
            string? profilePictureUrl = null,
            string? bio = null)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentException("Username cannot be empty");
            if (string.IsNullOrEmpty(email)) throw new ArgumentException("Email cannot be empty");
            if (string.IsNullOrEmpty(passwordHash)) throw new ArgumentException("Password hash cannot be empty");

            return new UserDto(id, username, email, passwordHash, profilePictureUrl, bio);
        }
    }