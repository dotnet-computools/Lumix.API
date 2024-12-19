namespace Lumix.Persistence.Entities;

public class Like
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PhotoId { get; set; }
    public DateTime CreatedAt { get; set; }

    public User? User { get; set; }
    public Photo? Photo { get; set; }
}