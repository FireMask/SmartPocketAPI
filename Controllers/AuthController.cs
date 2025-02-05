using Microsoft.AspNetCore.Mvc;
using SmartPocketAPI.Auth;
using SmartPocketAPI.Helpers;
using SmartPocketAPI.Helpers.Extensions;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartPocketAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IResult> Login(LoginRequest request)
    {
        var errorDetails = new Dictionary<string, string>();
        try
        {
            if (request.Alias.IsNullOrEmpty())
                errorDetails.Add("alias", "The alias field is required");
            if (request.Password.IsNullOrEmpty())
                errorDetails.Add("password", "The password field is required");

            if (errorDetails.Any())
                throw new ArgumentNullException("Required values missing");

            var user = await _authService.Login(request);
            if (user == null)
                throw new Exception("Credentials are incorrect");

            var token = _authService.GenerateJwtToken(user);

            return (new { Token = token }).ToApiResponse(Constants.AUTH_SUCCESS);
        }
        catch (ArgumentNullException ex)
        {
            var error = new ApiError(Constants.AUTH_ERROR, ex.Message, errorDetails);
            return ex.Message.ToApiError(error, Constants.AUTH_ERROR);
        }
        catch (Exception ex)
        {
            return ex.Message.ToApiError(Constants.AUTH_ERROR);
        }
    }

    [HttpGet("getHash")]
    public IResult TestHash(string pass)
    {
        return Results.Ok(PasswordHasher.HashPassword(pass));
    }
}
