using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NuGet.Protocol;
using SmartPocketAPI.Database;
using SmartPocketAPI.Helpers;
using SmartPocketAPI.Helpers.Extensions;
using SmartPocketAPI.Middlewares;
using SmartPocketAPI.Models;
using SmartPocketAPI.Options;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IUserService _usersContext;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService usersContext, ILogger<UserController> logger)
    {
        _usersContext = usersContext;
        _logger = logger;
    }
    
    [HttpGet("/Users")]
    public async Task<IResult> GetUsers()
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _usersContext.GetUsersAsync();
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpGet]
    public async Task<IResult> GetUser()
    {

        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _usersContext.GetUserByIdAsync(userId);

            return (new UserDto
            {
                Id = result.Id,
                Name = result.Name,
                Email = result.Email,
            }).ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpPost]
    public async Task<IResult> CreateUser(UserViewModel userViewModel)
    {
        try
        {
            if (userViewModel == null)
                return Results.BadRequest();

            var newUser = await _usersContext.CreateUserAsync(userViewModel);

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

    [HttpDelete]
    public async Task<IResult> DeleteUser(Guid userGuid)
    {
        try
        {
            var result = await _usersContext.DeleteUserAsync(userGuid);
            return result.ToApiResponse(Constants.DELETE_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.DELETE_ERROR);
        }
    }
}
