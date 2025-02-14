using SmartPocketAPI.Models;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Services.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetUsersAsync();
    Task<bool> DeleteUserAsync(UserDto user);
    Task<User> GetUserByIdAsync(Guid userGuid);
    Task<SimpleUserDto> UpdateUserAsync(UserUpdateDto userViewModel);
}
