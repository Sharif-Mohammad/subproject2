using Domain.Framework;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Frameworks
{
    public class BookmarkRepository : BaseRepository<Bookmark>,IBookmarkRepository
    {
        public BookmarkRepository(ApplicationDbContext context) : base(context) { }

        // CountAsync method that counts the total number of bookmarks for a specific user
    public async Task<int> CountAsync(Expression<Func<Bookmark, bool>> filter)
    {
        return await _context.Bookmarks
            .Where(filter)
            .CountAsync();
    }

        public async Task<bool> IsBookMarked(string userId, string movieId)
        {
            return await _context.Bookmarks.Where(x => x.UserId == userId && x.MovieId == movieId).CountAsync() > 0;
        }
    }
}
