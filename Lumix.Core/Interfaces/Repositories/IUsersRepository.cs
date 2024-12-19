using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Repositories;

public interface IUsersRepository
{
    Task Add(UserDto user);
    Task<UserDto> GetByEmail(string email);
    // Task<HashSet<Enums.Permission>> GetUserPermissions(Guid userId);
    Task<UserDto?> GetUserByRefreshToken(string refreshToken);
    Task AddRefreshToken(RefreshTokenDto refreshToken);
    Task<RefreshTokenDto?> GetRefreshToken(string token);
    Task<RefreshTokenDto?> GetRefreshTokenByUserId(Guid userId);
}