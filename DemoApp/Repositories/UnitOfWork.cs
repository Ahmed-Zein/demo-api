using AutoMapper;
using DemoApp.Data;
using DemoApp.Interfaces;

namespace DemoApp.Repositories;

public class UnitOfWork(ApplicationDbContext context, IMapper mapper) : IUnitOfWork
{
    private ICategoryRepository? _categoryRepository;
    private IProductRepository? _productRepository;

    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(context);
    public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(context);

    public async Task<int> SaveAsync()
    {
        return await context.SaveChangesAsync();
    }

    public IMapper Mapper { get; } = mapper;

    public void Dispose()
    {
        context.Dispose();
    }
}