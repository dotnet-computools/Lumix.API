
using Lumix.Application.Services;
using Lumix.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lumix.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<AuthService>();
		services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<ILikeService, LikeService>();
        services.AddScoped<ICommentService, CommentService>();
		return services;
    }
    
}