using Lumix.Core.Models;

namespace Lumix.Core.Interfaces.Repositories;

public interface IUsersRepository
{
    Task Add(User user);
    Task<User> GetByEmail(string email);
   // Task<HashSet<Enums.Permission>> GetUserPermissions(Guid userId);
    Task<User?> GetUserByRefreshToken(string refreshToken);
    Task AddRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken?> GetRefreshToken(string token);
    Task<RefreshToken?> GetRefreshTokenByUserId(Guid userId);
}