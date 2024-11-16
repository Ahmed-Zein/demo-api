using DemoApp.Interfaces;
using DemoApp.Models;
using Microsoft.AspNetCore.Identity;

namespace DemoApp.Services;

public class AuthService(UserManager<AppUser> userManager) : IAuthService
{
    public async Task<IdentityResult> Register(AppUser user, string password, string role= RolesConstants.User)
    {
        var userResult = await userManager.CreateAsync(user, password);
        if (!userResult.Succeeded)
            return userResult;

        var roleResult = await userManager.AddToRoleAsync(user, role);
        if (userResult.Succeeded) return IdentityResult.Success;

        await userManager.DeleteAsync(user);
        return userResult;
    }

    public async Task<bool> CheckPassword(AppUser user, string password)
    {
        return await userManager.CheckPasswordAsync(user, password);
    }
    
}