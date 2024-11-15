using DemoApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Data;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        List<Category> categories = [new() { Id = 1, Name = "Category 1" }, new() { Id = 2, Name = "Category 2" }];
        List<IdentityRole> roles =
        [
            new() { Name = Models.Roles.Admin, NormalizedName = Models.Roles.Admin.ToUpper() },
            new() { Name = Models.Roles.User, NormalizedName = Models.Roles.User.ToUpper() }
        ];
        modelBuilder.Entity<Category>().HasData(categories);
        modelBuilder.Entity<IdentityRole>().HasData(roles);
    }
}