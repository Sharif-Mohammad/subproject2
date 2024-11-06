using Business.Models.Common;
using Business.Models.Movies;
using Business.Models.Movies.Search;
using Business.Services.Movies;
using Business.Services.Movies.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace API.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieSearchController : ControllerBase
    {
        private readonly ISearchService searchService;

        public MovieSearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet("{userId}/string-search/{searchString}")]
        [Authorize]
        public async Task<IActionResult> StringSearch(
          [FromRoute] string userId,
        [FromRoute] string searchString,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
        {
            page = page > 0 ? page : 1;
            var request = new MovieSimpleSearchRequest
            {
                Query = searchString,
                UserId = userId,
                Offset =  ( page-1 ) * pageSize,
                Limit = pageSize
            };
            var searchResults = await searchService.StringSearchAsync(request);
            var paginatedResult = PaginatedResult<MovieSearchResultDto>.Create(searchResults, page, pageSize, $"movies/{userId}/string-search/{searchString}?");
            return Ok(paginatedResult);
        }


        [HttpGet("{userId}/structured-string-search")]
        [Authorize]
        public async Task<IActionResult> StructuredStringSearch(
    [FromRoute] string userId,
    [FromQuery] string? title = null,
    [FromQuery] string? plot = null,
    [FromQuery] string? characters = null,
    [FromQuery] string? personNames = null,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
        {
            page = page > 0 ? page : 1;

            var request = new StructuredStringSearchRequest
            {
                Title = title,
                Plot = plot,
                Characters = characters,
                PersonNames = personNames,
                UserId = userId,
                Offset = (page - 1) * pageSize,
                Limit = pageSize
            };

            page = page > 0 ? page : 1;

            var searchResults = await searchService.StructuredStringSearchAsync(request);
            var paginatedResult = PaginatedResult<MovieSearchResultDto>.Create(searchResults, page, pageSize, $"movies/{userId}/structured-string-search?title={title}&plot={plot}&characters={characters}&personNames={personNames}&");
            return Ok(paginatedResult);
        }


        [HttpGet("movie-search/{title}")]
        public async Task<IActionResult> FindMovieTitle(
    [FromRoute] string title,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
        {
            page = page > 0 ? page : 1;
            var request = new MovieSearchRequest
            {
                Title = title,
                Offset = (page - 1) * pageSize,
                Limit = pageSize
            };
            var searchResults = await searchService.FindMovieTitles(request);
            var paginatedResult = PaginatedResult<MovieSearchResultDto>.Create(searchResults, page, pageSize, $"movies/movie-search/{title}?");
            return Ok(paginatedResult);
        }

        [HttpGet("co-actors/{actorName}")]
        public async Task<IActionResult> FindCoPlayers(
    [FromRoute] string actorName,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
        {
            // Ensure the page starts from 1
            page = page > 0 ? page : 1;

            // Create the request model
            var request = new CoPlayerSearchRequest
            {
                ActorName = actorName,
                Offset = (page - 1) * pageSize,
                Limit = pageSize
            };

            // Call the search service
            var searchResults = await searchService.FindCoPlayers(request);

            // Create a paginated response
            var paginatedResult = PaginatedResult<CoPlayerResultDto>.Create(
                searchResults, page, pageSize, $"movies/co-actors/{actorName}?");

            return Ok(paginatedResult);
        }


        [HttpGet("{movieId}/actors-by-popularity")]
        public async Task<IActionResult> GetActorsByPopularity(
    [FromRoute] string movieId,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
        {
            // Ensure the page starts from 1
            page = page > 0 ? page : 1;

            // Create the request model
            var request = new ActorPopularitySearchRequest
            {
                MovieId = movieId,
                Offset = (page - 1) * pageSize,
                Limit = pageSize
            };

            // Call the search service
            var searchResults = await searchService.GetActorsByPopularityAsync(request);

            // Create a paginated response
            var paginatedResult = PaginatedResult<ActorPopularityResultDto>.Create(
                searchResults, page, pageSize, $"movies/{movieId}/actors-by-popularity?");

            return Ok(paginatedResult);
        }


        [HttpGet("{movieId}/similar")]
        public async Task<IActionResult> GetSimilarMovies(
    [FromRoute] string movieId,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
        {
            // Ensure page starts from 1
            page = page > 0 ? page : 1;

            // Set up the request model
            var request = new SimilarMoviesRequest
            {
                MovieId = movieId,
                Offset = (page - 1) * pageSize,
                Limit = pageSize
            };

            // Call the search service
            var similarMovies = await searchService.GetSimilarMoviesAsync(request);

            // Generate the paginated result
            var paginatedResult = PaginatedResult<SimilarMovieResultDto>.Create(
                similarMovies, page, pageSize, $"movies/{movieId}/similar?");

            return Ok(paginatedResult);
        }


        [HttpGet("exact-match")]
        public async Task<IActionResult> ExactMatch(
    [FromQuery] string[] keywords,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
        {
            // Ensure page starts from 1
            page = page > 0 ? page : 1;

            // Set up the request model
            var request = new ExactMatchRequest
            {
                Keywords = keywords,
                Offset = (page - 1) * pageSize,
                Limit = pageSize
            };

            // Call the search service
            var movies = await searchService.ExactMatchAsync(request);

            // Generate the paginated result
            var paginatedResult = PaginatedResult<MovieSearchResultDto>.Create(
                movies, page, pageSize, $"movies/exact-match?keywords={string.Join(",", keywords)}");

            return Ok(paginatedResult);
        }


    }
}
