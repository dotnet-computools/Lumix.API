
using System.Security.Claims;
using Lumix.Core.Models;

namespace Lumix.Application.Auth;

public interface IJwtProvider
{
    string GenerateAccessToken(User user);
    RefreshToken GenerateRefreshToken(Guid userId);
}