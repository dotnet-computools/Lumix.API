using AutoMapper;
using Lumix.Core.Models;
using Lumix.Persistence.Entities;
using User = Lumix.Core.Models.User;

namespace Lumix.Persistence;

public class DataBaseMappings : Profile
{
    public DataBaseMappings()
    {
        CreateMap<UserEntity, User>();
        CreateMap<RefreshTokenEntity, RefreshToken>();
    }
}