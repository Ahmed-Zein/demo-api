using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DemoApp.Dto.AppUser;

public class AppUserDto
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}

public class RegisterUserReq
{
    [Required, MinLength(2)] public string FirstName { get; set; }
    [Required, MinLength(2)] public string LastName { get; set; }
    [Required, EmailAddress] public string Email { get; set; }
    [Required, MinLength(6)] public string Password { get; set; }
}

public class LoginRequest
{
    [Required, EmailAddress] public string Email { get; set; }
    [Required] public string Password { get; set; }
}

public class LoginResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}