

using AutoMapper;
using Lumix.Core.DTOs;
using Lumix.Persistence.Entities;
using RefreshToken = Lumix.Persistence.Entities.RefreshToken;

namespace Lumix.Persistence;

public class DataBaseMappings : Profile
{
    public DataBaseMappings()
    {
        CreateMap<User, UserDto>();
        CreateMap<RefreshToken, RefreshTokenDto>();
        CreateMap<Photo, PhotoDto>();
        CreateMap<Like, LikeDto>();
        CreateMap<Comment, CommentDto>();
        CreateMap<Follow, FollowDto>();
        CreateMap<Tag, TagDto>();
        CreateMap<PhotoTag, PhotoTagDto>();
    }
}