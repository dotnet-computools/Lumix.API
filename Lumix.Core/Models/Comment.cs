namespace Lumix.Persistence.Entities
{
	public class Comment
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid PhotoId { get; set; }
		public string Text { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }
	}
}
