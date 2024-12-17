
using Lumix.Application.Auth;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Core.Interfaces.Services;
using Lumix.Core.Models;

public class UserService : IUserService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public UserService(
        IUsersRepository usersRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task Register(string userName, string email, string password)
    {
        var hashedPassword = _passwordHasher.Generate(password);

        var user = User.Create(
            Guid.NewGuid(),
            userName,
            hashedPassword,
            email);

        await _usersRepository.Add(user);
    }

    public async Task<(string AccessToken, string RefreshToken)> Login(string email, string password)
    {
        var user = await _usersRepository.GetByEmail(email);

        var result = _passwordHasher.Verify(password, user.PasswordHash);

        if (!result)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var accessToken = _jwtProvider.GenerateAccessToken(user);
        var existingRefreshToken = await _usersRepository.GetRefreshTokenByUserId(user.Id);

        if (existingRefreshToken == null || existingRefreshToken.ExpiresAt <= DateTime.UtcNow)
        {
            var newRefreshToken = _jwtProvider.GenerateRefreshToken(user.Id);
            await _usersRepository.AddRefreshToken(newRefreshToken);
            return (accessToken, newRefreshToken.Token);
        }

        return (accessToken, existingRefreshToken.Token);
    }

    public async Task<string> RefreshToken(string token)
    {
        var refreshToken = await _usersRepository.GetRefreshToken(token);

        if (refreshToken == null)
        {
            throw new InvalidOperationException("Refresh token not found");
        }

        if (refreshToken.ExpiresAt <= DateTime.UtcNow)
        {
            throw new InvalidOperationException("Refresh token expired");
        }

        var user = await _usersRepository.GetUserByRefreshToken(token);

        if (user == null)
        {
            throw new InvalidOperationException("User not found for the given refresh token");
        }

        var newAccessToken = _jwtProvider.GenerateAccessToken(user);

        return newAccessToken;
    }
}