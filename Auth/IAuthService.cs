using SmartPocketAPI.Models;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Auth;

public interface IAuthService
{
    Task<UserDto> Login(LoginRequest loginRequest);
    Task<User?> RegisterAsync(UserViewModel newUser);
    Task<bool> RecoverPasswordAsync(UserUpdateViewModel userViewModel);
    string GenerateJwtToken(UserDto user);
    Task<bool> Logout();

}
