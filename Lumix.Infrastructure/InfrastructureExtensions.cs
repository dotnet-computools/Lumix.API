using Lumix.Application.Auth;
using Lumix.Infrastructure.Authenfication;
using Microsoft.Extensions.DependencyInjection;

namespace Lumix.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        return services;
    }
}