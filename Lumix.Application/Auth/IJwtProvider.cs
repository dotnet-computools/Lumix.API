


using Lumix.Core.DTOs;

namespace Lumix.Application.Auth;

public interface IJwtProvider
{
    public string GenerateAccessToken(UserDto userDto);
    RefreshTokenDto GenerateRefreshToken(Guid userId);
}