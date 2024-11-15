using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DemoApp.Dto.AppUser;

public class AppUserDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [EmailAddress] public string Email { get; set; }
}

public class RegisterUserReq
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [EmailAddress] public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginRequest
{
    [EmailAddress] public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Token { get; set; }
}