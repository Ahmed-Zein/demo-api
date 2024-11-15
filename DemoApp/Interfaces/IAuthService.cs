using DemoApp.Models;
using Microsoft.AspNetCore.Identity;

namespace DemoApp.Services;

public interface IAuthService
{
    Task<IdentityResult> Register(AppUser user, string password, string role);
    Task<bool> CheckPassword(AppUser user, string password);
}