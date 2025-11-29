using Lumix.Core.DTOs;
namespace Lumix.Core.Interfaces.Repositories;

public interface IUsersRepository
{
    Task<UserDto> GetByEmail(string email);
    Task<UserDto> UpdateUsernameAsync(Guid userId, string username);
    Task<UserDto> GetByIdAsync(Guid userId);
    Task UpdateProfilePictureAsync(Guid userId, string profilePictureUrl);
    Task<UserProfileDto> GetProfileAsync(Guid userId);
}