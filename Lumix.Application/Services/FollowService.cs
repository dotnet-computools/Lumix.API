using Lumix.Application.Auth;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Core.Interfaces.Services;

namespace Lumix.Application.Services;

public class FollowService : IFollowService
{
    private readonly IFollowRepository _followRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public FollowService(
        IFollowRepository followRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _followRepository = followRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
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
}