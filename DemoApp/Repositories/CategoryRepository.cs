using DemoApp.Data;
using DemoApp.Interfaces;
using DemoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Repositories;

public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
{
    public async Task<bool> AnyAsync(int categoryId)
    {
        return await context.Categories.AnyAsync(c => c.Id == categoryId);
    }

    public async Task AddAsync(Category entity)
    {
        await context.Categories.AddAsync(entity);
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await context.Categories.ToListAsync();
    }

    public Task UpdateAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Category entity)
    {
        context.Categories.Remove(entity);
    }
}