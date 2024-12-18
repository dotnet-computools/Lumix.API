namespace Lumix.Core.Entities
{
	public class Photo
	{
		private Photo(Guid id, Guid userId, string title, string url, string tags, DateTime dateTimeNow, int likeCount)
		{
			Id = id;
			UserId = userId;
			Title = title;
			Url = url;
			Tags = tags;
			CreatedAt = dateTimeNow;
			LikeCount = likeCount;
		}

		public Guid Id { get; }
		public Guid UserId { get; }
		public string Title { get; private set; }
		public string Url { get; private set; }
		public string Tags { get; private set; }
		public DateTime CreatedAt { get; }
		public int LikeCount { get; private set; }

		public static Photo Create(Guid id, Guid userId, string title, string url, string tags, DateTime dateTimeNow, int likeCount)
		{
			return new Photo(id , userId, title, url, tags, dateTimeNow, likeCount);
		}
	}
}
