
using Microsoft.Extensions.DependencyInjection;

namespace Lumix.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<AuthService>();
        return services;
    }
    
}