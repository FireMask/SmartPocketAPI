using Microsoft.AspNetCore.Authorization;
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

[Authorize]
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

    [HttpDelete]
    public async Task<IResult> DeleteUser(UserDto user)
    {
        try
        {
            var result = await _usersContext.DeleteUserAsync(user);
            return result.ToApiResponse(Constants.DELETE_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.DELETE_ERROR);
        }
    }

    [HttpPut]
    public async Task<IResult> UpdateUserAsync(UserUpdateDto userUpdateViewModel)
    {
        try
        {
            userUpdateViewModel.Id = HttpContext.GetUserId();
            var result = await _usersContext.UpdateUserAsync(userUpdateViewModel);
            return result.ToApiResponse(Constants.UPDATE_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.UPDATE_SUCCESS);
        }
    }

    [HttpGet("/verifyToken")]
    public IResult VerifyToken()
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            return Results.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.UPDATE_SUCCESS);
        }
    }
}
