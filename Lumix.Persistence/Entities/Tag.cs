namespace Lumix.Persistence.Entities
{
	public class Tag
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }

		public List<PhotoTag> PhotoTags { get; set; } = [];
	}
}
