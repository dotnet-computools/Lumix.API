
using System.Security.Claims;
using Lumix.Core.Models;

namespace Lumix.Application.Auth;

public interface IJwtProvider
{
    string Generate(User user);
    RefreshToken GenerateRefreshToken(Guid userId);
}