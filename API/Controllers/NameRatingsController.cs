using Business.Models.Common;
using Business.Services.Movies.Search;
using Business.Services.Movies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Models.Movies;
using Business.Models.Movies.Search;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/name-ratings")]
    [ApiController]
    [Authorize]
    public class NameRatingsController : ControllerBase
    {
        private readonly INameRatingsService nameRatings;

        public NameRatingsController(INameRatingsService nameRatings)
        {
            this.nameRatings = nameRatings;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateNameRatings()
        {
            await nameRatings.UpdateNameRatings();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RateMovie([FromBody] RateMovieRequest request)
        {
            // Validate the input model
            if (request == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            try
            {
                // Call the rating service to rate the movie
                await nameRatings.Rate(request);
                return Ok("Rating submitted successfully.");
            }
            catch (ArgumentException ex)
            {
                // Handle specific argument exceptions (e.g., invalid rating value)
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{userId}/search/{name}")]
        [Authorize]
        public async Task<IActionResult> GetNameRating(
            [FromRoute] string userId,
            [FromRoute] string name,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            // Ensure page is valid
            page = page > 0 ? page : 1;

            var offset = (page - 1) * pageSize;
            var request = new NameRatingSearchRequest
            {
                Name = name,
                Offset = offset,
                Limit = pageSize
            };

            var searchResults = await nameRatings.GetNameRatingAsync(request);
            var paginatedResult = PaginatedResult<NameRatingDto>.Create(searchResults, page, pageSize, $"name-ratings/{userId}/search/{name}");
            return Ok(paginatedResult);
        }
    }
}
