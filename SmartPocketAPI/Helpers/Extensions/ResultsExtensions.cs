using SmartPocketAPI.Models;
using System.Collections;

namespace SmartPocketAPI.Helpers.Extensions;

public static class ResultsExtensions
{
    public static IResult ToApiResponse<T>(this T data, string message = "Operation successful", string code = "SUCCESS")
    {
        message = GetMessageForData(data, message);
        return Results.Ok(ApiResponse<T>.SuccessResponse(data, message, code));
    }

    public static IResult ToApiResponse<T>(this T data, string code = "SUCCESS")
    {
        string message = Constants.OK;
        message = GetMessageForData(data, message);
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

    private static string GetMessageForData<T>(T data, string defaultMessage)
    {
        if (data is null || data is IEnumerable enumerable && enumerable.IsNullOrEmpty())
        {
            return Constants.NO_DATA;
        }
        return defaultMessage;
    }
}
