namespace Lumix.Persistence.Entities
{
    public class Follow
    {
        public Guid Id { get; set; }
        public Guid FollowerId { get; set; }
        public Guid FollowingId { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserEntity? Follower { get; set; }
        public UserEntity? Following { get; set; }
    }
}