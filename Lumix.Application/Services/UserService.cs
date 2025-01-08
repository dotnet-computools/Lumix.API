using Lumix.Application.Auth;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Core.Interfaces.Services;

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
}