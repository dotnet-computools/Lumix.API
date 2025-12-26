namespace Lumix.Core.DTOs
{
	public class PhotoTagDto
	{
		public Guid Id { get; set; }
		public Guid TagId { get; set; }
		public Guid PhotoId { get; set; }
		public DateTime CreatedAt { get; set; }
		public TagDto? Tag { get; set; }

	}
}
