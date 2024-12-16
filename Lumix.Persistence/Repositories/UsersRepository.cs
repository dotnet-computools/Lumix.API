

using AutoMapper;
using Gallery.Persistence.Entities;
using Lumix.Core.Enums;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Core.Models;

public class UsersRepository : IUsersRepository
{
    private readonly LumixDbContext _context;
    private readonly IMapper _mapper;

    public UsersRepository(LumixDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Add(User user)
    {
        var roleEntity = await _context.Roles
                             .SingleOrDefaultAsync(r => r.Id == (int)Role.Admin)
                         ?? throw new InvalidOperationException();

        var userEntity = new UserEntity()
        {
            Id = user.Id,
            UserName = user.UserName,
            PasswordHash = user.PasswordHash,
            Email = user.Email,
           // Roles = { roleEntity }
        };

        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRefreshToken(RefreshToken refreshToken)
    {
        var refreshTokenEntity = new RefreshTokenEntity
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

    public Task UpdateRefreshToken(Guid userId, string refreshToken, DateTime expiration)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserByRefreshToken(string refreshToken)
    {
        var userEntity = await _context.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.Token == refreshToken && rt.ExpiresAt > DateTime.UtcNow));

        return userEntity is null ? null : _mapper.Map<User>(userEntity);
    }

    public async Task<User> GetByEmail(string email)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();

        return _mapper.Map<User>(userEntity);
    }

    public async Task<HashSet<Permission>> GetUserPermissions(Guid userId)
    {
        var roles = await _context.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == userId)
            .Select(u => u.Roles)
            .ToArrayAsync();

        return roles
            .SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(p => (Permission)p.Id)
            .ToHashSet();
    }
}