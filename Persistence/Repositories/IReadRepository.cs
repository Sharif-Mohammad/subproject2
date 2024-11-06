namespace Persistence.Repositories;

public interface IReadRepository<T, TKey> where T : class
{
    Task<T> GetByIdAsync(TKey id);
    Task<IEnumerable<T>> GetAllAsync(int page, int pageSize);
}