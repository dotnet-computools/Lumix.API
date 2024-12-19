namespace Lumix.Persistence.Entities;

public class LikeEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PhotoId { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserEntity? User { get; set; }
    public PhotoEntity? Photo { get; set; }
}