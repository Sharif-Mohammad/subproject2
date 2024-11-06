namespace Persistence.Repositories;

public interface IWriteRepository<T, TKey> where T : class
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(TKey id);
}