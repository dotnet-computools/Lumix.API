

using AutoMapper;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using RefreshToken = Lumix.Persistence.Entities.RefreshToken;

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
    
}
