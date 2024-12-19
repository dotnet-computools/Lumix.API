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

    public async Task Add(UserDto user)
    {
        // var roleEntity = await _context.Roles
        //                      .SingleOrDefaultAsync(r => r.Id == (int)Role.Admin)
        //                  ?? throw new InvalidOperationException();

        var userEntity = new User()
        {
            Id = user.Id,
            Username = user.Username,
            PasswordHash = user.PasswordHash,
            Email = user.Email,
            // Roles = { roleEntity }
        };

        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRefreshToken(RefreshToken refreshToken)
    {
        var existingToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken.Token);

        if (existingToken != null)
        {
            throw new InvalidOperationException("A refresh token with the same value already exists.");
        }

        var refreshTokenEntity = new RefreshToken
        {
            Id = refreshToken.Id,
            UserId = refreshToken.UserId,
            Token = refreshToken.Token,
            ExpiresAt = refreshToken.ExpiresAt,
            CreatedAt = refreshToken.CreatedAt
        };

        await _context.RefreshTokens.AddAsync(refreshTokenEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetRefreshToken(string token)
    {
        var refreshTokenEntity = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token);

        return refreshTokenEntity is null ? null : _mapper.Map<RefreshToken>(refreshTokenEntity);
    }
    public async Task<RefreshToken?> GetRefreshTokenByUserId(Guid userId)
    {
        var refreshTokenEntity = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.UserId == userId && rt.ExpiresAt > DateTime.UtcNow);

        return refreshTokenEntity is null ? null : _mapper.Map<RefreshToken>(refreshTokenEntity);
    }
    

/*    public async Task<User?> GetUserByRefreshToken(string refreshToken)
    {
        var userEntity = await _context.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.Token == refreshToken && rt.ExpiresAt > DateTime.UtcNow));

        return userEntity is null ? null : _mapper.Map<User>(userEntity);
    }*/

    public async Task<User> GetByEmail(string email)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email) ?? throw new InvalidOperationException("User not found.");

        return _mapper.Map<User>(userEntity);
    }

	Task<UserDto> IUsersRepository.GetByEmail(string email)
	{
		throw new NotImplementedException();
	}

	public Task AddRefreshToken(RefreshTokenDto refreshToken)
	{
		throw new NotImplementedException();
	}

	Task<RefreshTokenDto?> IUsersRepository.GetRefreshToken(string token)
	{
		throw new NotImplementedException();
	}

	Task<RefreshTokenDto?> IUsersRepository.GetRefreshTokenByUserId(Guid userId)
	{
		throw new NotImplementedException();
	}

	public Task<UserDto?> GetUserByRefreshToken(string refreshToken)
	{
		throw new NotImplementedException();
	}

	// public async Task<HashSet<Permission>> GetUserPermissions(Guid userId)
	// {
	//     var roles = await _context.Users
	//         .AsNoTracking()
	//         .Include(u => u.Roles)
	//         .ThenInclude(r => r.Permissions)
	//         .Where(u => u.Id == userId)
	//         .Select(u => u.Roles)
	//         .ToArrayAsync();
	//
	//     return roles
	//         .SelectMany(r => r)
	//         .SelectMany(r => r.Permissions)
	//         .Select(p => (Permission)p.Id)
	//         .ToHashSet();
	// }
}