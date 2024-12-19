namespace Lumix.Core.Models
{
	public class Follow
	{
		private Follow(
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

		public static Follow Create(
			Guid id,
			Guid followerId,
			Guid followingId)
		{
			if (followerId == followingId)
				throw new ArgumentException("A user cannot follow themselves");

			return new Follow(id, followerId, followingId);
		}
	}
}
