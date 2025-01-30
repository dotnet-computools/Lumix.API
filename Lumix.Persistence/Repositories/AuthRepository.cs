using AutoMapper;
using Lumix.Application.Auth;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lumix.Persistence.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly LumixDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public AuthRepository(
        LumixDbContext context,
        IMapper mapper,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _context = context;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task Register(UserDto user)
    {
        try
        {
            var userEntity = new User
            {
                Id = Guid.NewGuid(),
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.InnerException?.Message);
            throw;
        }
    }

    public async Task<(string AccessToken, string RefreshToken)> Login(string email, string password)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email) 
            ?? throw new InvalidOperationException("User not found.");

        var result = _passwordHasher.Verify(password, userEntity.PasswordHash);

        if (!result)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var user = _mapper.Map<UserDto>(userEntity);
        var accessToken = _jwtProvider.GenerateAccessToken(user);
        
        var existingRefreshToken = await _context.RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(rt => rt.UserId == user.Id && rt.ExpiresAt > DateTime.UtcNow);

        if (existingRefreshToken == null || existingRefreshToken.ExpiresAt <= DateTime.UtcNow)
        {
            var newRefreshToken = _jwtProvider.GenerateRefreshToken(user.Id);
            
            var refreshTokenEntity = new RefreshToken
            {
                Id = newRefreshToken.Id,
                UserId = newRefreshToken.UserId,
                Token = newRefreshToken.Token,
                ExpiresAt = newRefreshToken.ExpiresAt,
                CreatedAt = newRefreshToken.CreatedAt
            };

            await _context.RefreshTokens.AddAsync(refreshTokenEntity);
            await _context.SaveChangesAsync();
            
            return (accessToken, newRefreshToken.Token);
        }

        return (accessToken, existingRefreshToken.Token);
    }

    public async Task<string> RefreshToken(string token)
    {
        var refreshTokenEntity = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token);

        if (refreshTokenEntity == null)
        {
            throw new InvalidOperationException("Refresh token not found");
        }

        if (refreshTokenEntity.ExpiresAt <= DateTime.UtcNow)
        {
            throw new InvalidOperationException("Refresh token expired");
        }

        var userEntity = await _context.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.Token == token && rt.ExpiresAt > DateTime.UtcNow));

        if (userEntity == null)
        {
            throw new InvalidOperationException("User not found for the given refresh token");
        }

        var user = _mapper.Map<UserDto>(userEntity);
        var newAccessToken = _jwtProvider.GenerateAccessToken(user);

        return newAccessToken;
    }

    public async Task AddRefreshToken(RefreshTokenDto refreshTokenDto)
    {
        var existingToken = await _context.RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(rt => rt.Token == refreshTokenDto.Token);

        if (existingToken != null)
        {
            throw new InvalidOperationException("A refresh token with the same value already exists.");
        }

        var refreshTokenEntity = new RefreshToken
        {
            Id = refreshTokenDto.Id,
            UserId = refreshTokenDto.UserId,
            Token = refreshTokenDto.Token,
            ExpiresAt = refreshTokenDto.ExpiresAt,
            CreatedAt = refreshTokenDto.CreatedAt
        };

        await _context.RefreshTokens.AddAsync(refreshTokenEntity);
        await _context.SaveChangesAsync();
    }
}