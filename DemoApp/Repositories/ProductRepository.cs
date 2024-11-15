using DemoApp.Data;
using DemoApp.Interfaces;
using DemoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Repositories;

public interface IProductRepository : IRepository<Product, int>
{
    Task<List<Product>> GetAllAsync(int categoryId);
}

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    public async Task<bool> AnyAsync(int productId)
    {
        return await context.Products.AnyAsync(p => p.Id == productId);
    }

    public async Task AddAsync(Product entity)
    {
        await context.Products.AddAsync(entity);
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<List<Product>> GetAllAsync(int categoryId)
    {
        return await context.Products.Where(p => p.Id == categoryId).ToListAsync();
    }

    public Task UpdateAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Product entity)
    {
        context.Products.Remove(entity);
    }
}