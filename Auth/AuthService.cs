
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartPocketAPI.Database;
using SmartPocketAPI.Models;
using SmartPocketAPI.Options;
using SmartPocketAPI.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace SmartPocketAPI.Auth;

public class AuthService : IAuthService
{
    public JwtOptions _options { get; }
    public ApplicationDbContext _context { get; }

    public AuthService(ApplicationDbContext context, IOptions<JwtOptions> options)
    {
        _options = options.Value;
        _context = context;
    }

    public string GenerateJwtToken(UserDto user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<UserDto?> Login(LoginRequest loginRequest)
    {
        if (loginRequest == null)
            return null;

        User? user = await _context.Users
            .FirstOrDefaultAsync(user => user.Email == loginRequest.Email);

        if (user == null)
            return null;

        if (!PasswordHasher.VerifyPassword(loginRequest.Password, user.Password))
            return null;

        return new UserDto()
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
        };
    }

    public async Task<User?> RegisterAsync(UserViewModel uservm)
    {
        if (_context.Users.Any(user => user.Email == uservm.Email))
            throw new Exception("The email already exists");

        var address = string.Empty;
        try
        {
            address = new MailAddress(uservm.Email).Address;
        }
        catch (Exception ex) { }

        if (address.IsNullOrEmpty())
            throw new ArgumentException("The email field is incorrect");

        User user = new User
        {
            Id = Guid.NewGuid(),
            Name = uservm.Name,
            Password = PasswordHasher.HashPassword(uservm.Password),
            Email = address
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<bool> RecoverPasswordAsync(UserUpdateViewModel userViewModel)
    {

        User? user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userViewModel.Email);
        if (user == null)
            throw new Exception("There is no account with this email");

        if (userViewModel.VerifyCode.IsNullOrEmpty())
        {
            user.VerifyCode = "1234";
            SendMailCode(user);
        }
        else
        {
            if (user.VerifyCode != userViewModel.VerifyCode)
                throw new Exception("The verify code is incorrect or is expired");

            user.Password = PasswordHasher.HashPassword(userViewModel.Password);
            user.VerifyCode = string.Empty;
        }

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return true;
    }

    private bool SendMailCode(User user)
    {
        //TODO: Send mail with the code
        //user.Email;

        return true;
    }

    public Task<bool> Logout()
    {
        throw new NotImplementedException();
    }
}
