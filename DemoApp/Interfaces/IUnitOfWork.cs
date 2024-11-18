using AutoMapper;
using DemoApp.LoggerHub;
using DemoApp.Repositories;
using DemoApp.Services;
using Microsoft.AspNetCore.SignalR;

namespace DemoApp.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository CategoryRepository { get; }
    IProductRepository ProductRepository { get; }
    IAuthService AuthService { get; }
    Task<int> SaveAsync();
    IMapper Mapper { get; }
    IHubContext<LoggerHub.LoggerHub, ILoggerHubClient> LoggerHub { get; }
}