using SmartPocketAPI.Models;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Services.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetUsersAsync();
    Task<User?> CreateUserAsync(UserViewModel newUser);
    Task<bool> DeleteUserAsync(Guid userId);
    Task<User> GetUserByIdAsync(Guid userGuid);
}
