using Microsoft.AspNetCore.Mvc;

namespace SmartPocketAPI.ViewModels;

public class UserViewModel
{
    public string Alias { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class UserDto
{
    public Guid Id { get; set; }
    public string Alias { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}