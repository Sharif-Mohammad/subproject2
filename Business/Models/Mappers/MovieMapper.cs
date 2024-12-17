using Business.Models.Movies;
using Domain.Entities.Movies;

namespace Business.Models.Mappers;

public static class MovieMapper
{
    public static MovieDto ToMovieDto(this Movie m)
    {
        return new MovieDto
        {
            MovieId = m.MovieId,
            Title = m.Title,
            ReleaseYear = m.ReleaseYear,
            RuntimeMinutes = m.RuntimeMinutes,
            IsAdult = m.IsAdult,
            Plot = m.Plot,
            SelfUrl = $"/api/movies/{m.MovieId}",
            PosterUrl = m.PosterUrl,
        };
    }
}