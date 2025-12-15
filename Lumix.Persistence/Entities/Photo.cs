namespace Lumix.Persistence.Entities;

public class Photo
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    public string Url { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int LikeCount { get; set; }
    public bool IsAvatar { get; set; }

    public User? User { get; set; }
    public List<Like> Likes { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
    public List<PhotoTag> PhotoTags { get; set; } = new();
}