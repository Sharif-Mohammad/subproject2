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

    // New Search endpoint
    [HttpGet("simple-search")]
    public async Task<IActionResult> SearchMovies(
        [FromQuery] string title = null,
        [FromQuery] string plot = null,
        [FromQuery] int? releaseYear = null,
        [FromQuery] bool? isAdult = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
         var movies = await movieService.SearchAsync(title,plot,releaseYear,isAdult,page,pageSize);
        return Ok(movies);
    }
}
