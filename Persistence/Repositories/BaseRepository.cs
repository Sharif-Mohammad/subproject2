using Dapper;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{

    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
    Expression<Func<TEntity, bool>> filter = null,
    params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            // Apply filter if provided
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Apply includes for related entities
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task<int> ExecuteNonQueryAsync(string sql, params object[] parameters)
        {
            return await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public virtual async Task<IEnumerable<T>> ExecuteRawSqlAsync<T>(string sql, object parameters = null) where T : class
        {
            var connection = _context.Database.GetDbConnection();
            // Avoid using 'using' so that DbContext manages the connection lifecycle
            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }
            return await connection.QueryAsync<T>(sql, parameters);
        }
    }

}
