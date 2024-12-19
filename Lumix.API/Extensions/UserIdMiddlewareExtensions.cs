using Lumix.API.Middleware;

namespace Lumix.API.Extensions;

public static class UserIdMiddlewareExtensions
{
    public static IApplicationBuilder UseUserId(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UserIdMiddleware>();
    }

    public static Guid? GetUserId(this HttpContext context)
    {
        return context.Items["UserId"] as Guid?;
    }
}