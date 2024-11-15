using DemoApp.Models;

namespace DemoApp.Interfaces;

public interface ITokenService
{
    string GenerateToken(AppUser user, IList<string> roles);
}