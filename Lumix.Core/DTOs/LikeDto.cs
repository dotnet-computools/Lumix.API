namespace Lumix.Core.Models
{
	public class LIkeDto
	{
		private LIkeDto(
			Guid id,
			Guid userId,
			Guid photoId)
		{
			Id = id;
			UserId = userId;
			PhotoId = photoId;
			CreatedAt = DateTime.UtcNow;
		}

		public Guid Id { get; }
		public Guid UserId { get; }
		public Guid PhotoId { get; }
		public DateTime CreatedAt { get; }

		public static LIkeDto Create(
			Guid id,
			Guid userId,
			Guid photoId)
		{
			return new LIkeDto(id, userId, photoId);
		}
	}
}
