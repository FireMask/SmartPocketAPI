using System.Security.Claims;

namespace SmartPocketAPI.Extensions;

public static class StringExtensions
{

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