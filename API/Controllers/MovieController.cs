using Business.Services.Movies;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController(IMovieService movieService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMovies([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var movies = await movieService.GetMoviesAsync(page, pageSize);
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieById(string id)
    {
        var movie = await movieService.GetMovieByIdAsync(id);
        if (movie == null) return NotFound();

        return Ok(movie);
    }
}
