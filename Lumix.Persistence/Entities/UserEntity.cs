

using Lumix.Persistence.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    // public List<RoleEntity> Roles { get; set; } = new();
    public List<RefreshTokenEntity> RefreshTokens { get; set; } = new();
}