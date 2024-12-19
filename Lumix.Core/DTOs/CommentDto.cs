namespace Lumix.Core.Models
{
	public class CommentDto
	{
		private CommentDto(
			Guid id,
			Guid userId,
			Guid photoId,
			string text)
		{
			Id = id;
			UserId = userId;
			PhotoId = photoId;
			Text = text;
			CreatedAt = DateTime.UtcNow;
		}

		public Guid Id { get; }
		public Guid UserId { get; }
		public Guid PhotoId { get; }
		public string Text { get; } = string.Empty;
		public DateTime CreatedAt { get; }

		public static CommentDto Create(
			Guid id,
			Guid userId,
			Guid photoId,
			string text)
		{
			if (string.IsNullOrEmpty(text)) throw new ArgumentException("Comment text cannot be empty");

			return new CommentDto(id, userId, photoId, text);
		}
	}
}
