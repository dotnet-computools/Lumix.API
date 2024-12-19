namespace Lumix.API.Middleware;

public class UserIdMiddleware
{
    private readonly RequestDelegate _next;
    private const string UserIdClaimType = "userId";

    public UserIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var userIdClaim = context.User.FindFirst(UserIdClaimType);
        
        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
        {
            context.Items["UserId"] = userId;
        }

        await _next(context);
    }
}