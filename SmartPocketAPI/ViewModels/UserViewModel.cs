using Microsoft.AspNetCore.Mvc;

namespace SmartPocketAPI.ViewModels;

//public Guid Id { get; set; }
//public string Email { get; set; } = string.Empty;
//public string Password { get; set; } = string.Empty;
//public string Name { get; set; } = string.Empty;
//public string VerifyCode { get; set; } = string.Empty;
//public bool IsPremium { get; set; } = false;

public class UserViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class UserUpdateViewModel
{
    public string VerifyCode { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class UserUpdateDto
{
    public Guid Id { get; set; }
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsPremium { get; set; } = false;
}

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsPremium { get; set; } = false;
}

public class SimpleUserDto
{
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
