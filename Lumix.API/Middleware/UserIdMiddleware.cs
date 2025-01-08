namespace Lumix.API.Middleware;

/// <summary>
/// Middleware to extract the user ID from the claims and add it to the HttpContext items.
/// </summary>
public class UserIdMiddleware
{
    private readonly RequestDelegate _next;
    private const string UserIdClaimType = "userId";

    
    public UserIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes the middleware to extract the user ID from the claims and add it to the HttpContext items.
    /// </summary>
    /// <param name="context">The HttpContext for the current request.</param>
    /// <returns>A task that represents the completion of request processing.</returns>
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