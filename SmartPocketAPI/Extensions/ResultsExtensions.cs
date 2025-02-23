using SmartPocketAPI.ApiResponse;
using SmartPocketAPI.Helpers;
using System.Collections;

namespace SmartPocketAPI.Extensions;

public static class ResultsExtensions
{
    public static IResult ToApiResponse<T>(this T data, string message = "Operation successful", string code = "SUCCESS")
    {
        return Results.Ok(ApiResponse<T>.SuccessResponse(data, message, code));
    }

    public static IResult ToApiResponse<T>(this T data, string code = "SUCCESS")
    {
        string message = Constants.OK;
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
}
