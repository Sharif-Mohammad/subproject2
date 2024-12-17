using Business.Models.Common;
using Business.Models.Movies;

namespace Business.Services.Movies;

public interface IMovieService
{
    Task<PaginatedResult<MovieDto>> GetMoviesAsync(int page, int pageSize);
    Task<MovieDto> GetMovieByIdAsync(string movieId);

     Task<PaginatedResult<MovieDto>> SearchAsync(string title = null, string plot = null, int? releaseYear = null, bool? isAdult = null, int page = 1, int pageSize = 10);
}