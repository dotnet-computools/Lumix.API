using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Services;

public interface IFollowService
{
    Task FollowUser(Guid userId, Guid targetUserId);
    Task<bool> IsFollowing(Guid userId, Guid targetUserId);
    Task<List<Guid>> GetFollowing(Guid userId);
}