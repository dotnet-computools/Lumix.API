using System;

namespace Lumix.Core.DTOs
{
	public class PhotoDto
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }

		public string Title { get; set; } = string.Empty;
		public string Url { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }

		public int LikeCount { get; set; }
		public bool IsAvatar { get; set; }

        public List<LikeDto> Likes { get; set; } = new();
		public List<CommentDto> Comments { get; set; } = new();
		public List<PhotoTagDto> PhotoTags { get; set; } = new();

	}
}
