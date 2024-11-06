using Business.Models.Frameworks;
using Domain;
using Persistence.Query;
using Persistence.Repositories;
using Persistence.Repositories.Frameworks;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Frameworks
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IUnitOfWork _bookmarkRepository;

        public BookmarkService(IUnitOfWork bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        public async Task<bool> Add(string userId, string movieId)
        {
            await _bookmarkRepository.Bookmarks.AddAsync(new Domain.Framework.Bookmark
            {
                UserId = userId,
                MovieId = movieId,
                CreatedAt = DateTime.UtcNow,
            });
            return await _bookmarkRepository.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<UserBookmarkDto>> GetUserBookmarksAsync(string userId)
        {
            var bookMarks = await _bookmarkRepository.Bookmarks.GetAllAsync(
                         note => note.UserId == userId,
                         note => note.User
                     );

            return bookMarks.Select(b => new UserBookmarkDto
            {
                MovieId = b.MovieId,
                BookmarkDate = b.CreatedAt,
            });
        }
    }

}
