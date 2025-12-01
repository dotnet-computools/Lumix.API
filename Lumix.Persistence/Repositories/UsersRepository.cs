using AutoMapper;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lumix.Persistence.Repositories;

public class UsersRepository : IUsersRepository
{
   
    private readonly LumixDbContext _context;
    private readonly IMapper _mapper;

    public UsersRepository(LumixDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto> GetByEmail(string email)
    {
        var userEntity = await _context.Users
                             .AsNoTracking()
                             .FirstOrDefaultAsync(u => u.Email == email) 
                         ?? throw new InvalidOperationException("User not found.");

        return _mapper.Map<UserDto>(userEntity);
    }
    
    public async Task<UserDto> UpdateUsernameAsync(Guid userId, string username)
    {
        var user = await _context.Users.FindAsync(userId) 
                   ?? throw new InvalidOperationException("User not found");

        // Check unique username
        if (await _context.Users.AnyAsync(u => u.Username == username && u.Id != userId))
        {
            throw new InvalidOperationException("Username already taken");
        }

        user.Username = username;
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetByIdAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId)
            ?? throw new InvalidOperationException("User not found");
        return _mapper.Map<UserDto>(user);
    }

    public async Task UpdateProfilePictureAsync(Guid userId, string profilePictureUrl)
    {
        var user = await _context.Users
                        .FirstOrDefaultAsync(u => u.Id == userId)
                        ?? throw new InvalidOperationException("User not found");
        user.ProfilePictureUrl = profilePictureUrl;
        await _context.SaveChangesAsync();
    }

    public async Task<UserProfileDto> GetProfileAsync(Guid userId)
    {
        var user = await _context.Users
            .Include(u => u.Photos)
            .Include(u => u.Followers)
            .Include(u => u.Following)
            .FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new InvalidOperationException("User not found");

        return new UserProfileDto
        {
            Id = user.Id,
            Username = user.Username,
            ProfilePictureUrl = user.ProfilePictureUrl,
            Bio = user.Bio,
            FollowersCount = user.Followers.Count,
            FollowingCount = user.Following.Count,
            PhotosCount = user.Photos.Count,
            Photos = user.Photos
            .OrderByDescending(p => p.CreatedAt)
            .Where(p => !p.IsAvatar)
            .Select(p => new PhotoPrewiewDto
            {
                Id = p.Id,
                Url = p.Url
            })
            .ToList()
        };
    }
}
