


using Lumix.Application.Auth;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Core.Interfaces.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public AuthService(
        IAuthRepository authRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _authRepository = authRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task Register(string userName, string email, string password)
    {
        var hashedPassword = _passwordHasher.Generate(password);
      
        var user = UserDto.Create(
            Guid.NewGuid(),
            userName,
            email,
            hashedPassword);
        
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("Username cannot be empty");
            
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty");
            
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty");
        await _authRepository.Register(user);
    }

    public async Task<(string AccessToken, string RefreshToken)> Login(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty");
            
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty");
        return await _authRepository.Login(email, password);
    }

    public async Task<string> RefreshToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token cannot be empty");
        return await _authRepository.RefreshToken(token);
    }

    public async Task AddRefreshToken(RefreshTokenDto refreshTokenDto)
    {
        await _authRepository.AddRefreshToken(refreshTokenDto);
    }
}