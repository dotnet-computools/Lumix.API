using Lumix.Application.Auth;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Core.Interfaces.Services;

public class UserService : IUserService
{
    private readonly IUsersRepository _usersRepository;
    public UserService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
     
    }
    
    public async Task<UserDto> UpdateUsernameAsync(Guid userId, string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username cannot be empty");
        }

        if (username.Length < 3)
        {
            throw new ArgumentException("Username must be at least 3 characters long");
        }

        return await _usersRepository.UpdateUsernameAsync(userId, username);
    }

    public async Task<UserProfileDto> GetProfileAsync(Guid userId)
    {
        var user = await _usersRepository.GetByIdAsync(userId);

        return new UserProfileDto
        {
            Id = user.Id,
            Username = user.Username,
            ProfilePictureUrl = user.ProfilePictureUrl,
            Bio = user.Bio,
            FollowersCount = user.Followers.Count(),
            FollowingCount = user.Following.Count(),
            PhotosCount = user.Photos.Count(),
            Photos = user.Photos
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new PhotoPrewiewDto
                {
                    Id = p.Id,
                    Url = p.Url
                })
                .ToList()
        };
    }
}