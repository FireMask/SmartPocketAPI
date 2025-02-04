
using Microsoft.EntityFrameworkCore;
using SmartPocketAPI.Auth;
using SmartPocketAPI.Database;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Services;

public class UserService : IUserService
{
    public ApplicationDbContext _context { get; }
    public ILogger<UserService> _log { get; }

    public UserService(ApplicationDbContext context, ILogger<UserService> logger)
    {
        _context = context;
        _log = logger;
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        return await _context.Users
            .Select(x => new UserDto
                {
                    Id = x.Id,
                    Alias = x.Alias,
                    Email = x.Email,
                    Name = x.Name,
                }
            )
            .ToListAsync();
    }

    public async Task<User?> CreateUserAsync(UserViewModel uservm)
    {
        if (_context.Users.Any(user => user.Alias == uservm.Alias))
            return null;

        User user = new User
        {
            Id = Guid.NewGuid(),
            Alias = uservm.Alias,
            Name = uservm.Name,
            Password = PasswordHasher.HashPassword(uservm.Password),
            Email = uservm.Email
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<bool> DeleteUserAsync(Guid userGuid)
    {
        try
        {
            if (userGuid == Guid.Empty)
                throw new Exception("No se proporciono un id");

            User? user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userGuid);

            if (user == null)
                throw new Exception("El usuario no existe");

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _log.LogError(ex.ToString());
            return false;
        }

        return true;
    }

    public async Task<User> GetUserByIdAsync(Guid userGuid)
    {
        if (userGuid == Guid.Empty)
            throw new ArgumentNullException(nameof(userGuid));

        User user = await _context.Users.FirstAsync(x => x.Id == userGuid);

        if (user == null)
            throw new Exception("User does not exists");

        return user;
    }

}