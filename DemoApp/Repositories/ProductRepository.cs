using DemoApp.Data;
using DemoApp.Interfaces;
using DemoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Repositories;

public interface IProductRepository : IRepository<Product, int>
{
    Task<List<Product>> GetAllAsync(int categoryId);
    Task AddAsync(Product entity, int categoryId);
}

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    public async Task<bool> AnyAsync(int productId)
    {
        return await context.Products.AsNoTracking()
            .AnyAsync(p => p.Id == productId);
    }

    public async Task AddAsync(Product entity)
    {
        await context.Products.AddAsync(entity);
    }

    public async Task AddAsync(Product entity, int categoryId)
    {
        entity.CategoryId = categoryId;
        await context.Products.AddAsync(entity);
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await context.Products.AsNoTracking()
            .Include(p => p.Category).AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<List<Product>> GetAllAsync(int categoryId)
    {
        return await context.Products.Where(p => p.Id == categoryId).AsNoTracking().ToListAsync();
    }

    public async Task<Product?> UpdateAsync(int id, Product toUpdate)
    {
        var product = await context.Products.FindAsync(id);

        if (product is null)
            return null;

        product.Name = toUpdate.Name;
        return product;
    }

    public void Delete(Product entity)
    {
        context.Products.Remove(entity);
    }
}