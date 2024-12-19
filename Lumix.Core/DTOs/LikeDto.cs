namespace Lumix.Core.DTOs
{
	public class LikeDto
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid PhotoId { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
