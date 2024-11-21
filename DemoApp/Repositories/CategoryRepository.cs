using DemoApp.Data;
using DemoApp.Dto.Categories;
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

    public async Task<List<Category>> GetAllAsync()
    {
        return await context.Categories.ToListAsync();
    }

    public async Task<Category?> UpdateAsync(int id, Category toUpdate)
    {
        var category = await context.Categories.FindAsync(id);
        if (category is null)
            return null;
        category.Name = toUpdate.Name;
        return category;
    }

    public void Delete(Category entity)
    {
        context.Categories.Remove(entity);
    }
}