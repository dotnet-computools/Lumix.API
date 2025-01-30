namespace Lumix.API.Contracts.Request.AuthRequest;

public class RefreshTokenRequest
{
    public string RefreshToken { get; set; } = string.Empty;
}