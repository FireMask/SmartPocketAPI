using Microsoft.AspNetCore.Mvc;
using SmartPocketAPI.Auth;
using SmartPocketAPI.Helpers;
using SmartPocketAPI.Helpers.Extensions;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interface;
using SmartPocketAPI.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartPocketAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IResult> Login(LoginRequest request)
    {
        var errorDetails = new Dictionary<string, string>();
        try
        {
            if (request.Email.IsNullOrEmpty())
                errorDetails.Add("email", "The email field is required");
            if (request.Password.IsNullOrEmpty())
                errorDetails.Add("password", "The password field is required");

            if (errorDetails.Any())
                throw new ArgumentNullException("Required values missing");

            var user = await _authService.Login(request);

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

    [HttpPost("recover")]
    public async Task<IResult> RecoverUserPassword(UserUpdateViewModel userUpdateViewModel)
    {
        try
        {
            var result = await _authService.RecoverPasswordAsync(userUpdateViewModel);
            return result.ToApiResponse(Constants.UPDATE_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.UPDATE_SUCCESS);
        }
    }

    [HttpPost("create")]
    public async Task<IResult> CreateUser(UserViewModel userViewModel)
    {
        try
        {
            if (userViewModel == null)
                throw new ArgumentNullException(nameof(userViewModel));

            var newUser = await _authService.RegisterAsync(userViewModel);

            if (newUser == null)
                throw new Exception("Error creating user");

            return newUser.ToApiResponse(Constants.INSERT_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.INSERT_ERROR);
        }
    }

    [HttpGet("getHash")]
    public IResult TestHash(string pass)
    {
        return Results.Ok(PasswordHasher.HashPassword(pass));
    }
}
