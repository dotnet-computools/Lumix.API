namespace Lumix.Persistence.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Bio { get; set; }

    public List<Photo> Photos { get; set; } = [];
    public List<Like> Likes { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];
    public List<Follow> Followers { get; set; } = [];
    public List<Follow> Following { get; set; } = [];
    public List<RefreshToken> RefreshTokens { get; set; } = [];
}