

using Lumix.Core.Models;
using Lumix.Persistence.Entities;

namespace Lumix.Application.Auth;

public interface IJwtProvider
{
    public string GenerateAccessToken(User user);
    RefreshTokenDto GenerateRefreshToken(Guid userId);
}