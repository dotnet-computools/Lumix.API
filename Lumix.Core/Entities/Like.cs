namespace Lumix.Core.Entities
{
	public class Like
	{
		private Like(Guid id, Guid userId, Guid photoId, DateTime dateTimeNow)
		{
			Id = id;
			UserId = userId;
			PhotoId = photoId;
			CreatedAt = dateTimeNow;
		}

		public Guid Id { get; }
		public Guid UserId { get; }
		public Guid PhotoId { get; }
		public DateTime CreatedAt { get; }

		public static Like Create(Guid id, Guid userId, Guid photoId, DateTime dateTimeNow)
		{
			return new Like(id, userId, photoId, dateTimeNow);
		}
	}
}
