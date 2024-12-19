using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Repositories;

public interface IUsersRepository
{
	Task<UserDto> GetByEmail(string email);

}