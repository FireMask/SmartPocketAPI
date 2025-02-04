using Microsoft.AspNetCore.Mvc;
using SmartPocketAPI.Auth;
using SmartPocketAPI.Services.Interface;

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
    public async Task<IResult> Login([FromBody] LoginRequest request)
    {

        var user = await _authService.Login(request);
        if(user == null)
            return Results.Unauthorized();
        
        var token = _authService.GenerateJwtToken(user);
        return Results.Ok(new { Token = token }); 
    }

    [HttpGet("getHash")]
    public IResult TestHash(string pass)
    {
        return Results.Ok(PasswordHasher.HashPassword(pass));
    }
}
