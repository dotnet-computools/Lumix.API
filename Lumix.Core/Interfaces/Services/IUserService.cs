using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Services;

public interface IUserService
{
    Task<UserDto> UpdateUsernameAsync(Guid userId, string username);
    Task<UserProfileDto> GetProfileAsync(Guid userId);
}