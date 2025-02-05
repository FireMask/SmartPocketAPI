using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NuGet.Protocol;
using SmartPocketAPI.Database;
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
        var users = await _usersContext.GetUsersAsync();
        return Results.Json(users);
    }

    [HttpGet]
    public async Task<IResult> GetUser()
    {

        try
        {
            Guid userId = HttpContext.GetUserId();
            User user = await _usersContext.GetUserByIdAsync(userId);
            return Results.Ok(new UserDto
            {
                Id = user.Id,
                Alias = user.Alias,
                Name = user.Name,
                Email = user.Email,
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return Results.NoContent();
    }

    [HttpPost]
    public async Task<IResult> CreateUser(UserViewModel userViewModel)
    {
        if (userViewModel == null)
            return Results.BadRequest();

        User? newUser = await _usersContext.CreateUserAsync(userViewModel);

        if (newUser == null)
            return Results.BadRequest();
        else
            return Results.Ok(newUser);
    }

    [HttpDelete]
    public async Task<IResult> DeleteUser(Guid userGuid)
    {

        if (await _usersContext.DeleteUserAsync(userGuid))
            return Results.Ok();
        else
            return Results.BadRequest();
    }
}
