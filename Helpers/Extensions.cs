using Microsoft.IdentityModel.Tokens;
using SmartPocketAPI.Models;
using System.Collections;
using System.Security.Claims;

namespace SmartPocketAPI.Helpers;

public static class Extensions
{
    public static IResult ToApiResponse<T>(this T data, string message = "Operation successful", string code = "SUCCESS")
    {
        return Results.Ok(ApiResponse<T>.SuccessResponse(data, message, code));
    }

    public static IResult ToApiResponse<T>(this T data, string code = "SUCCESS")
    {
        string message = Constants.OK;
        if (data is null || (data is IEnumerable enumerable && enumerable.IsNullOrEmpty()))
        {
            message = Constants.NO_DATA;
        }
        return Results.Ok(ApiResponse<T>.SuccessResponse(data, message, code));
    }

    public static IResult ToApiError(this string errorMessage, string code = "ERROR")
    {
        var error = new ApiError(code, errorMessage);
        return Results.BadRequest(ApiResponse<object>.ErrorResponse(errorMessage, code, error));
    }

    public static IResult ToApiError(this string errorMessage, ApiError error, string code = "ERROR")
    {
        return Results.BadRequest(ApiResponse<object>.ErrorResponse(errorMessage, code, error));
    }

    public static bool IsNullOrEmpty(this IEnumerable enumerable)
    {
        return enumerable == null || !enumerable.Cast<object>().Any();
    }

    public static Guid GetUserId(this HttpContext context)
    {
        if (!context.Items.ContainsKey("UserId"))
            throw new ArgumentException();

        string? userid = context.Items["UserId"] as string;

        if (userid == null)
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