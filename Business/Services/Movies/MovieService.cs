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
        var totalItems = await unitOfWork.Movies.GetCountAsync();

        var movieDtos = enumerable.Select(m => m.ToMovieDto()).ToList();

        return PaginatedResult<MovieDto>.Create(movieDtos, page, pageSize,"movies", totalItems);
    }

    public async Task<MovieDto> GetMovieByIdAsync(string movieId)
    {
        var movie = await unitOfWork.Movies.GetByIdAsync(movieId);
        return movie.ToMovieDto();
    }
    public async Task<PaginatedResult<MovieDto>> SearchAsync(string title = null, string plot = null, int? releaseYear = null, bool? isAdult = null, int page = 1, int pageSize = 10)
    {
        var ( movies, total) = await unitOfWork.Movies.SearchAsync(title,plot,releaseYear,isAdult,page,pageSize);

        var movieDtos = movies.Select(m => m.ToMovieDto()).ToList();

        return PaginatedResult<MovieDto>.Create(movieDtos, page, pageSize,"movies", total);
    }


}