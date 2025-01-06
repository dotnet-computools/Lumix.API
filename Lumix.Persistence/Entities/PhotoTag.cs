namespace Lumix.Persistence.Entities
{
	public class PhotoTag
	{
		public Guid Id { get; set; }
		public Guid TagId { get; set; }
		public Guid PhotoId { get; set; }
		public DateTime CreatedAt { get; set; }

		public Tag? Tag { get; set; }
		public Photo? Photo { get; set; }
	}
}
