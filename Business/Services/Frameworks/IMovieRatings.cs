using Business.Models.Movies;

namespace Business.Services.Frameworks
{
    public interface IMovieRatings
    {
        Task<RatingsDto> Get(string movieId);

        Task<UserRatingsDto> GetUserRatings(string userId, string movieId);
    }
}