using Business.Models.Common;
using Business.Models.Movies;

namespace Business.Services.Movies;

public interface IMovieService
{
    Task<PaginatedResult<MovieDto>> GetMoviesAsync(int page, int pageSize);
    Task<MovieDto> GetMovieByIdAsync(string movieId);
}