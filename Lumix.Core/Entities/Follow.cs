namespace Lumix.Core.Entities
{
	public class Follow
	{
		private Follow(Guid id, Guid followerId, Guid followingId, DateTime dateTimeNow)
		{
			Id = id;
			FollowerId = followerId;
			FollowingId = followingId;
			CreatedAt = dateTimeNow;
		}

		public Guid Id { get; }
		public Guid FollowerId { get; }
		public Guid FollowingId { get; }
		public DateTime CreatedAt { get; }

		public static Follow Create(Guid id, Guid followerId, Guid followingId, DateTime dateTimeNow)
		{
			return new Follow(id, followerId, followingId, dateTimeNow);
		}
	}
}
