
using System.Data.Common;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        Task<int> ExecuteNonQueryAsync(string sql, params object[] parameters);
        Task<IEnumerable<T>> ExecuteRawSqlAsync<T>(string sql, object parameters = null) where T : class;
        Task<IEnumerable<TEntity>> GetAllAsync(
    Expression<Func<TEntity, bool>> filter = null,
    params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetByIdAsync(object id);
        void Update(TEntity entity);
    }
}