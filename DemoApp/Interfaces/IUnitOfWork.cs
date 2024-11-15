using AutoMapper;
using DemoApp.Repositories;

namespace DemoApp.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository CategoryRepository { get; }
    IProductRepository ProductRepository { get; }
    Task<int> SaveAsync();
    IMapper Mapper { get; }
}