using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Repositories;

public interface IFollowRepository
{
    Task FollowUser(Guid userId, Guid targetUserId);
    Task<bool> IsFollowing(Guid userId, Guid targetUserId);
    Task<List<Guid>> GetFollowing(Guid userId);
}