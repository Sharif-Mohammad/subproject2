using Dapper;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public abstract class QuerySearch
    {
        private readonly ApplicationDbContext context;

        protected QuerySearch(ApplicationDbContext context)
        {
            this.context = context;
        }
        public virtual async Task<IEnumerable<T>> ExecuteRawSqlAsync<T>(string sql, object parameters = null) where T : class
        {
            var connection = context.Database.GetDbConnection();
            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }
            return await connection.QueryAsync<T>(sql, parameters);
        }

        public virtual async Task ExecuteNonQueryAsync(string sql, params object[] parameters)
        {
            var connection = context.Database.GetDbConnection();
            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }

            // Execute the command and don't expect any result
            await connection.ExecuteAsync(sql, parameters);
        }
    }
}
