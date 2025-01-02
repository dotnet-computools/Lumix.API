using System;

namespace Lumix.Core.DTOs
{
	public class PhotoDto
	{
		private readonly List<LikeDto> _likes = new();
		private readonly List<CommentDto> _comments = new();
		private readonly List<PhotoTagDto> _photoTags = new();

		private PhotoDto(
			Guid id,
			Guid userId,
			string title,
			string url,
			DateTime createdAt)
		{
			Id = id;
			UserId = userId;
			Title = title;
			Url = url;
			CreatedAt = createdAt;
			LikeCount = 0;
		}

		public Guid Id { get; }
		public Guid UserId { get; }
		public string Title { get; private set; } = string.Empty;
		public string Url { get; } = string.Empty;
		public DateTime CreatedAt { get; }
		public int LikeCount { get; private set; }

		public IReadOnlyList<LikeDto> Likes => _likes;
		public IReadOnlyList<CommentDto> Comments => _comments;
		public IReadOnlyList<PhotoTagDto> PhotoTags => _photoTags;

		public static PhotoDto Create(
			Guid id,
			Guid userId,
			string title,
			string url,
			DateTime createdAt)
		{
			if (string.IsNullOrEmpty(title)) throw new ArgumentException("Title cannot be empty");
			if (string.IsNullOrEmpty(url)) throw new ArgumentException("URL cannot be empty");
			if (title.Length > 200) throw new ArgumentException("Caption can't be longer than 500 characters");

			return new PhotoDto(id, userId, title, url, createdAt);
		}

		public void Update(
			string title)
		{
			if (string.IsNullOrEmpty(title)) throw new ArgumentException("Title cannot be empty");
			if (title.Length > 200) throw new ArgumentException("Caption can't be longer than 500 characters");

			Title = title;
		}

		public void IncrementLikeCount() => LikeCount++;
		public void DecrementLikeCount() => LikeCount = Math.Max(0, LikeCount - 1);
	}
}
