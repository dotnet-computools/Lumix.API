namespace Lumix.Persistence.Entities;

public class Photo
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? Tags { get; set; }
    public int LikeCount { get; set; }

    public User? User { get; set; }
    public List<Like> Likes { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];
}