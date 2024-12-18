namespace Lumix.Core.Entities
{
	public class Comment
	{
		private Comment(Guid id, Guid userId, Guid photoId, string text, DateTime dateTimeNow)
		{
			Id = id;
			UserId = userId;
			PhotoId = photoId;
			Text = text;
			CreatedAt = dateTimeNow;
		}

		public Guid Id { get; }
		public Guid UserId { get; }
		public Guid PhotoId { get; }
		public string Text { get; }
		public DateTime CreatedAt { get; }

		public static Comment Create(Guid id, Guid userId, Guid photoId, string text, DateTime dateTimeNow)
		{
			return new Comment(id, userId, photoId, text, dateTimeNow);
		}
	}
}
