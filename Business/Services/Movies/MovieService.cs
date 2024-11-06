using Business.Models.Common;
using Business.Models.Mappers;
using Business.Models.Movies;
using Domain.Entities.Movies;
using Persistence.Repositories;

namespace Business.Services.Movies;

public class MovieService(IUnitOfWork unitOfWork) : IMovieService
{
    public async Task<PaginatedResult<MovieDto>> GetMoviesAsync(int page, int pageSize)
    {
        var movies = await unitOfWork.Movies.GetAllAsync(page, pageSize);
        var enumerable = movies as Movie[] ?? movies.ToArray();
        var totalItems = enumerable.Length;

        var movieDtos = enumerable.Select(m => m.ToMovieDto()).ToList();

        return PaginatedResult<MovieDto>.Create(movieDtos, page, pageSize,"movies");
    }

    public async Task<MovieDto> GetMovieByIdAsync(string movieId)
    {
        var movie = await unitOfWork.Movies.GetByIdAsync(movieId);
        return movie.ToMovieDto();
    }


}