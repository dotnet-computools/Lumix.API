using Lumix.Core.DTOs;

namespace Lumix.Application.Auth;

public interface IJwtProvider
{
    string GenerateAccessToken(UserDto user);
    RefreshTokenDto GenerateRefreshToken(Guid userId);
}