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
            new() { Name = Models.RolesConstants.Admin, NormalizedName = Models.RolesConstants.Admin.ToUpper() },
            new() { Name = Models.RolesConstants.User, NormalizedName = Models.RolesConstants.User.ToUpper() }
        ];
        modelBuilder.Entity<Category>().HasData(categories);
        modelBuilder.Entity<IdentityRole>().HasData(roles);
        modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
    }
}