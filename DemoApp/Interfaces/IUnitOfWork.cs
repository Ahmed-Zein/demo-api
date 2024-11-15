using AutoMapper;
using DemoApp.Repositories;
using DemoApp.Services;

namespace DemoApp.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository CategoryRepository { get; }
    IProductRepository ProductRepository { get; }
    IAuthService AuthService { get; }
    Task<int> SaveAsync();
    IMapper Mapper { get; }
}