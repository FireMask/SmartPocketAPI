using SmartPocketAPI.Models;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Auth;

public interface IAuthService
{
    Task<UserDto?> Login(LoginRequest loginRequest);
    Task<bool> Logout();
    string GenerateJwtToken(UserDto user);
}
