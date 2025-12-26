namespace Lumix.Persistence.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PhotoId { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public Guid? ParentId { get; set; }
    public Comment? Parent { get; set; }
    public List<Comment> Children { get; set; } = new List<Comment>();

    public User? User { get; set; }
    public Photo? Photo { get; set; }
}