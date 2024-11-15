using DemoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        List<Category> categories = [new() { Id = 1, Name = "Category 1" }, new() { Id = 2, Name = "Category 2" }];
        modelBuilder.Entity<Category>().HasData(categories);
    }
}