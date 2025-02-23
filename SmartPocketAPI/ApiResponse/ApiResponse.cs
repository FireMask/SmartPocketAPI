namespace SmartPocketAPI.ApiResponse;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public ApiError? Error { get; set; }
    public string Message { get; set; }
    public string Code { get; set; }

    public ApiResponse(bool success, T data = default, ApiError? error = null, string message = "", string code = "")
    {
        Success = success;
        Data = data;
        Error = error;
        Message = message;
        Code = code;
    }

    public static ApiResponse<T> SuccessResponse(T data, string message = "", string code = "SUCCESS")
    {
        return new ApiResponse<T>(true, data, null, message, code);
    }

    public static ApiResponse<T> ErrorResponse(string message, string code = "ERROR", ApiError? error = null)
    {
        return new ApiResponse<T>(false, default, error, message, code);
    }
}

public class ApiError
{
    public string Code { get; set; }
    public string Details { get; set; }
    public Dictionary<string, string> Fields { get; set; }

    public ApiError(string code, string details = "", Dictionary<string, string> fields = null)
    {
        Code = code;
        Details = details;
        Fields = fields ?? new Dictionary<string, string>();
    }
}