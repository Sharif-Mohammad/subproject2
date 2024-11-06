using Business.Models.Movies.Search;
using Business.Services.Movies.Search;

namespace Business.Services.Movies
{
    public interface ISearchService
    {
        Task<IEnumerable<MovieSearchResultDto>> StringSearchAsync(MovieSimpleSearchRequest searchRequest);
        Task<IEnumerable<MovieSearchResultDto>> StructuredStringSearchAsync(StructuredStringSearchRequest searchRequest);
        Task<IEnumerable<MovieSearchResultDto>> FindMovieTitles(MovieSearchRequest searchRequest);

        Task<IEnumerable<CoPlayerResultDto>> FindCoPlayers(CoPlayerSearchRequest searchRequest);

        Task<IEnumerable<ActorPopularityResultDto>> GetActorsByPopularityAsync(ActorPopularitySearchRequest searchRequest);
        Task<IEnumerable<SimilarMovieResultDto>> GetSimilarMoviesAsync(SimilarMoviesRequest searchRequest);

        Task<IEnumerable<MovieSearchResultDto>> ExactMatchAsync(ExactMatchRequest searchRequest);
    }
}