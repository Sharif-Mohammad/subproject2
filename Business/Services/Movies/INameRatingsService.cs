
using Business.Models.Movies;
using Business.Models.Movies.Search;

namespace Business.Services.Movies
{
    public interface INameRatingsService
    {
        Task UpdateNameRatings();

        Task Rate(RateMovieRequest request);

        Task<IEnumerable<NameRatingDto>> GetNameRatingAsync(NameRatingSearchRequest searchRequest);
    }
}