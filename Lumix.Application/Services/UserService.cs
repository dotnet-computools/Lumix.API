using Lumix.Application.Auth;
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


}
