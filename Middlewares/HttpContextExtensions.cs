using SmartPocketAPI.Models;
using System.Security.Claims;

namespace SmartPocketAPI.Middlewares;

public static class HttpContextExtensions
{
    public static Guid GetUserId(this HttpContext context)
    {
        if (!context.Items.ContainsKey("UserId"))
            throw new ArgumentException();

        string? userid = context.Items["UserId"] as string;

        if(userid == null)
            throw new ArgumentException();

        return new Guid(userid);
    }

    public static string GetUserAlias(this HttpContext context)
    {
        if (!context.Items.ContainsKey("UserAlias"))
            throw new ArgumentException();

        string? useralias = context.Items["UserAlias"] as string;

        if (useralias == null)
            throw new ArgumentException();

        return useralias;
    }

    public static string GetClaimValue(this HttpContext context, string claimType)
    {
        return context.User?.FindFirstValue(claimType);  // Access directly from User claims
    }
}
