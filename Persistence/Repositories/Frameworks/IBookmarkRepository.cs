using Domain.Framework;
using System.Linq.Expressions;

namespace Persistence.Repositories.Frameworks
{
    public interface IBookmarkRepository : IBaseRepository<Bookmark>
    {
        Task<int> CountAsync(Expression<Func<Bookmark, bool>> filter);

        Task<bool> IsBookMarked(string userId, string movieId);
    }
}
