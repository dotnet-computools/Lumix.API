namespace Lumix.Core.DTOs
{
	public class PhotoDto
	{
		private readonly List<LikeDto> _likes = [];
		private readonly List<CommentDto> _comments = [];

		private PhotoDto(
			Guid id,
			Guid userId,
			string title,
			string url,
			string? tags = null)
		{
			Id = id;
			UserId = userId;
			Title = title;
			Url = url;
			CreatedAt = DateTime.UtcNow;
			Tags = tags;
			LikeCount = 0;
		}

		public Guid Id { get; }
		public Guid UserId { get; }
		public string Title { get; } = string.Empty;
		public string Url { get; } = string.Empty;
		public DateTime CreatedAt { get; }
		public string? Tags { get; }
		public int LikeCount { get; private set; }

		public IReadOnlyList<LikeDto> Likes => _likes;
		public IReadOnlyList<CommentDto> Comments => _comments;

		public static PhotoDto Create(
			Guid id,
			Guid userId,
			string title,
			string url,
			string? tags = null)
		{
			if (string.IsNullOrEmpty(title)) throw new ArgumentException("Title cannot be empty");
			if (string.IsNullOrEmpty(url)) throw new ArgumentException("URL cannot be empty");
			if (title.Length > 200) throw new ArgumentException("Caption can't be longer than 500 characters");
			if (tags?.Length > 500) throw new ArgumentException("Tags can't be longer than 500 characters");

			return new PhotoDto(id, userId, title, url, tags);
		}

		public void IncrementLikeCount() => LikeCount++;
		public void DecrementLikeCount() => LikeCount = Math.Max(0, LikeCount - 1);
	}
}
