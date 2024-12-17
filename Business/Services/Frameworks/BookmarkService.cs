using Business.Models.Common;
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


        public async Task<PaginatedResult<UserBookmarkDto>> GetUserBookmarksAsync(string userId, int pageNumber, int pageSize, string apiBasePath)
        {
            // Calculate the number of items to skip
            int skip = (pageNumber - 1) * pageSize;

            // Fetch bookmarks with pagination
            var bookMarks = await _bookmarkRepository.Bookmarks.GetAllAsync(
                note => note.UserId == userId,
                                skip: skip,
                take: pageSize,
                note => note.User

            );

            // Count the total number of bookmarks for the user
            var totalItems = await _bookmarkRepository.Bookmarks.CountAsync(note => note.UserId == userId);



            // Map to DTO
            var bookmarkDtos = bookMarks.Select(b => new UserBookmarkDto
            {
                MovieId = b.MovieId,
                BookmarkDate = b.CreatedAt,
            }).ToList();
            var allBookmarkedMovies = await _bookmarkRepository.Movies.GetMoviesByIdsAsync(bookmarkDtos.Select(b => b.MovieId).ToArray());
            var movies = from b in bookmarkDtos
                         join m in allBookmarkedMovies on b.MovieId equals m.MovieId
                         select new UserBookmarkDto
                         {
                             MovieId = m.MovieId,
                             BookmarkDate = m.CreatedAt,
                             Movie = new Models.Movies.MovieDto
                             {
                                 IsAdult = m.IsAdult,
                                 MovieId = m.MovieId.ToString(),
                                 Plot = m.Plot,
                                 PosterUrl = m.PosterUrl,
                                 ReleaseYear = m.ReleaseYear,
                                 RuntimeMinutes = m.RuntimeMinutes,
                                 Title = m.Title,

                             }
                         };

            // Return the paginated result
            var paginatedResult = PaginatedResult<UserBookmarkDto>.Create(movies, pageNumber, pageSize, apiBasePath,totalItems);

            return paginatedResult;
        }


        public async Task<bool> IsBookMarked(string userId, string movieId)
        {            
            return await _bookmarkRepository.Bookmarks.IsBookMarked(userId, movieId);
        }

        public async Task<bool> Remove(string userId, string movieId)
        {
           var bookmarks = await _bookmarkRepository.Bookmarks.GetAllAsync(x => x.UserId == userId && x.MovieId == movieId);
            foreach (var bookmark in bookmarks) {
               _bookmarkRepository.Bookmarks.Delete(bookmark);
            }
            return await _bookmarkRepository.CompleteAsync() > 0;
        }
    }

}
