using Business.Models.Common;
using Business.Models.Frameworks;

namespace Business.Services.Frameworks
{
    public interface IBookmarkService
    {
        Task<bool> Add(string userId, string movieId);
        Task<PaginatedResult<UserBookmarkDto>> GetUserBookmarksAsync(string userId, int pageNumber, int pageSize, string apiBasePath);
        Task<bool> IsBookMarked(string userId, string movieId);

        Task<bool> Remove(string userId, string movieId);
    }
}