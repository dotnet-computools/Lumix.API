namespace Lumix.Core.DTOs
{
	public class FollowDto
	{
		private FollowDto(
			Guid id,
			Guid followerId,
			Guid followingId)
		{
			Id = id;
			FollowerId = followerId;
			FollowingId = followingId;
			CreatedAt = DateTime.UtcNow;
		}

		public Guid Id { get; }
		public Guid FollowerId { get; }
		public Guid FollowingId { get; }
		public DateTime CreatedAt { get; }

		public static FollowDto Create(
			Guid id,
			Guid followerId,
			Guid followingId)
		{
			if (followerId == followingId)
				throw new ArgumentException("A user cannot follow themselves");

			return new FollowDto(id, followerId, followingId);
		}
	}
}
