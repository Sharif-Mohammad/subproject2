using Domain.Entities.Movies;

namespace Persistence.Repositories.Movies;

public interface IMovieRepository : IReadRepository<Movie, string>
{
    Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre);

    Task<int> GetCountAsync();

    Task<(IList<Movie>, int total)> SearchAsync(string title = null, string plot = null, int? releaseYear = null, bool? isAdult = null, int page = 1, int pageSize = 10);

    Task<IEnumerable<Movie>> GetMoviesByIdsAsync(params string[] moviesId);
}