namespace Lumix.Core.DTOs
{
	public class PhotoTagDto
	{
		private PhotoTagDto(
			Guid id,
			Guid tagId,
			Guid photoId,
			DateTime createdAt,
            TagDto? tag = null)
		{
			Id = id;
			TagId = tagId;
			PhotoId = photoId;
			CreatedAt = createdAt;
            Tag = tag;
        }

		public Guid Id { get; }
		public Guid TagId { get; }
		public Guid PhotoId { get; }
		public DateTime CreatedAt { get; }
		public TagDto? Tag { get; }

        public static PhotoTagDto Create(
			Guid id, 
			Guid tagId, 
			Guid photoId, 
			DateTime createdAt,
            TagDto? tag = null)
		{
			return new PhotoTagDto(id, tagId, photoId, createdAt, tag);
		}
	}
}
