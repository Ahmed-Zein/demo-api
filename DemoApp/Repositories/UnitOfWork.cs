using AutoMapper;
using DemoApp.Data;
using DemoApp.Interfaces;
using DemoApp.Models;
using DemoApp.Services;
using Microsoft.AspNetCore.Identity;

namespace DemoApp.Repositories;

public class UnitOfWork(ApplicationDbContext context, UserManager<AppUser> userManager, IMapper mapper) : IUnitOfWork
{
    private ICategoryRepository? _categoryRepository;
    private IProductRepository? _productRepository;
    private IAuthService? _authService;

    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(context);
    public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(context);
    public IAuthService AuthService => _authService ??= new AuthService(userManager);

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