namespace DemoApp.Interfaces;

public interface IRepository<T, P>
{
    Task<bool> AnyAsync(P id);
    Task AddAsync(T entity);
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task UpdateAsync(T entity);
    void Delete(T entity);
}