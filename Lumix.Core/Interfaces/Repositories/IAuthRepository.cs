using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Repositories;

public interface IAuthRepository
{
    Task Register(UserDto user);
    Task<(string AccessToken, string RefreshToken)> Login(string email, string password);
    Task<string> RefreshToken(string token);

    Task AddRefreshToken(RefreshTokenDto refreshTokenDto);
}