using SmartPocketAPI.Auth;
using System.Security.Claims;

namespace SmartPocketAPI.Middlewares;

public class UserInfoMiddleware
{
    private readonly RequestDelegate _next;

    public UserInfoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var user = context.User;
        if (user?.Identity?.IsAuthenticated == true)
        {
            context.Items["UserId"] = user.FindFirstValue(ClaimTypes.NameIdentifier);
            context.Items["UserAlias"] = user.FindFirstValue(ClaimTypes.Name);
        }

        await _next(context);
    }
}
