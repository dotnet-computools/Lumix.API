using AutoMapper;
using Lumix.Core.DTOs;
using Lumix.Persistence.Entities;

namespace Lumix.Persistence;

public class DataBaseMappings : Profile
{
    public DataBaseMappings()
    {
        CreateMap<User, UserDto>();
        CreateMap<RefreshToken, RefreshTokenDto>();
    }
}