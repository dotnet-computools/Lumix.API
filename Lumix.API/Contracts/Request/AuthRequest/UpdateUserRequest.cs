namespace Lumix.API.Contracts.Request.AuthRequest;

public class UpdateUserRequest
{
    public string? Username { get; init; }
    public IFormFile? Avatar { get; init; }
}

