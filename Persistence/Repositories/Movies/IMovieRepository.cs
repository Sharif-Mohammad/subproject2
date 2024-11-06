using Domain.Entities.Movies;

namespace Persistence.Repositories.Movies;

public interface IMovieRepository : IReadRepository<Movie, string>
{
    Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre);
}