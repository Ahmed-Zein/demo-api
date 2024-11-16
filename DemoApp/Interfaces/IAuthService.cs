using DemoApp.Models;
using Microsoft.AspNetCore.Identity;

namespace DemoApp.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> Register(AppUser user, string password, string role);
    Task<AppUser?> ValidateCredentials(string email, string password);
    Task<IList<string>> GetRoles(AppUser user);
}