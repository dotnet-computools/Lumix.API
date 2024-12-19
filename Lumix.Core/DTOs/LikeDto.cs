namespace Lumix.Core.DTOs
{
	public class LikeDto
	{
		private LikeDto(
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

		public static LikeDto Create(
			Guid id,
			Guid userId,
			Guid photoId)
		{
			return new LikeDto(id, userId, photoId);
		}
	}
}
