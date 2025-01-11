using Lumix.Application.Auth;
using Lumix.Application.PhotoUpload;
using Lumix.Infrastructure.Authenfication;
using Lumix.Infrastructure.Authenfication.Jwt;
using Lumix.Infrastructure.PhotoUpload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Lumix.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IFileStorageService, S3BucketService>();
        services.AddScoped<IPhotoFileValidationService, PhotoFileValidationService>();
        return services;
    }
}