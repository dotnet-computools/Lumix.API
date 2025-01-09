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
}