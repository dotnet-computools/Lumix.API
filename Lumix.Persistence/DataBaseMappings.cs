

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
        CreateMap<User, UserPreviewDto>();
        CreateMap<RefreshToken, RefreshTokenDto>();
        CreateMap<Photo, PhotoDto>()
            .ForMember(d => d.Author, o => o.MapFrom(s => s.User));
        CreateMap<Like, LikeDto>();
        CreateMap<Comment, CommentDto>();
        CreateMap<Follow, FollowDto>();
        CreateMap<Tag, TagDto>()
            .ForMember(d => d.PhotoTags, o => o.Ignore());
        CreateMap<PhotoTag, PhotoTagDto>();
        CreateMap<FollowDto, Follow>();
    }
}