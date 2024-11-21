using DemoApp.Models;

namespace DemoApp.Interfaces;

public interface IRepository<T, P>
{
    Task<bool> AnyAsync(P id);
    Task AddAsync(T entity);
    Task<T?> GetByIdAsync(int id);

    Task<List<T>> GetAllAsync();

    //Task UpdateAsync(T toUpdate);
    public Task<T?> UpdateAsync(int id, T toUpdate);
    void Delete(T entity);
}