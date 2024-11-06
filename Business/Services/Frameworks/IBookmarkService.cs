using Business.Models.Frameworks;

namespace Business.Services.Frameworks
{
    public interface IBookmarkService
    {
        Task<bool> Add(string userId, string movieId);
        Task<IEnumerable<UserBookmarkDto>> GetUserBookmarksAsync(string userId);
    }
}