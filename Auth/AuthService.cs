
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartPocketAPI.Database;
using SmartPocketAPI.Models;
using SmartPocketAPI.Options;
using SmartPocketAPI.ViewModels;
using System.IdentityModel.Tokens.Jwt;
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
            .Where(user => user.Email == loginRequest.Email)
            .FirstOrDefaultAsync();


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

    public Task<bool> Logout()
    {
        throw new NotImplementedException();
    }
}
