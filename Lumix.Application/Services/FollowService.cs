using Lumix.Application.Auth;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Core.Interfaces.Services;

namespace Lumix.Application.Services;

public class FollowService : IFollowService
{
    private readonly IFollowRepository _followRepository;
    public FollowService(IFollowRepository followRepository)
    {
        _followRepository = followRepository;
    }

    public async Task FollowUser(Guid userId, Guid targetUserId)
    {
        await _followRepository.FollowUser(userId, targetUserId);
    }

    public async Task<bool> IsFollowing(Guid userId, Guid targetUserId)
    {
        return await _followRepository.IsFollowing(userId, targetUserId);
    }

    public async Task<List<Guid>> GetFollowing(Guid userId)
    {
        return await _followRepository.GetFollowing(userId);
    }
    
    public async Task UnfollowUser(Guid userId, Guid targetUserId) 
    {
        await _followRepository.UnfollowUser(userId, targetUserId);
    }
    public async Task<List<Guid>> GetFollowers(Guid userId)
    {
        return await _followRepository.GetFollowers(userId);
    }
}