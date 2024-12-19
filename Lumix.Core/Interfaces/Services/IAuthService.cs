using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Services;

public interface IAuthService
{
    Task Register(string userName, string email, string password);
    Task<(string AccessToken, string RefreshToken)> Login(string email, string password);
    Task<string> RefreshToken(string token);
    Task AddRefreshToken(RefreshTokenDto refreshTokenDto);
}