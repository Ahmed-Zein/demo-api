
using Microsoft.AspNetCore.Identity;

namespace DemoApp.Models;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    void t()
    {
    }
}