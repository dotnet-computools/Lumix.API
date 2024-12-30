namespace Lumix.Core.DTOs
{
	public class PhotoTagDto
	{
		private PhotoTagDto(
			Guid id,
			Guid tagId,
			Guid photoId,
			DateTime createdAt)
		{
			Id = id;
			TagId = tagId;
			PhotoId = photoId;
			CreatedAt = createdAt;
		}

		public Guid Id { get; }
		public Guid TagId { get; }
		public Guid PhotoId { get; }
		public DateTime CreatedAt { get; }

		public static PhotoTagDto Create(
			Guid id, 
			Guid tagId, 
			Guid photoId, 
			DateTime createdAt)
		{
			return new PhotoTagDto(id, tagId, photoId, createdAt);
		}
	}
}
