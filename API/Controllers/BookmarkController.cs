using Business.Models.Frameworks;
using Business.Services.Frameworks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/bookmarks")]
    [Authorize]
    [ApiController]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarkController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [HttpPost]
        public async Task<IActionResult> BookmarkMovie([FromBody] BookmarkRequest request)
        {
            if (string.IsNullOrEmpty(request.UserId) || string.IsNullOrEmpty(request.MovieId))
            {
                return BadRequest("User ID and Movie ID are required.");
            }

            var success = await _bookmarkService.Add(request.UserId, request.MovieId);

            if (success)
                return CreatedAtAction(nameof(BookmarkMovie), new { userId = request.UserId, movieId = request.MovieId });

            return Conflict("Bookmark already exists.");
        }


        // Get all notes for a specific user
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetBookmarksForUser(string userId,        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
        {
            var notes = await _bookmarkService.GetUserBookmarksAsync(userId, page, pageSize,$"users/{userId}");
            return Ok(notes);
        }

        // Get all notes for a specific user
        [HttpGet("users/{userId}/{movieId}")]
        public async Task<IActionResult> GetBookmarksForUser(string userId, string movieId)
        {
            var isBookMarked = await _bookmarkService.IsBookMarked(userId, movieId);
            return Ok(new
            {
                IsBookmarked = isBookMarked,
            });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] BookmarkRequest request)
        {
            if (string.IsNullOrEmpty(request.UserId) || string.IsNullOrEmpty(request.MovieId))
            {
                return BadRequest("User ID and Movie ID are required.");
            }

            var success = await _bookmarkService.Remove(request.UserId, request.MovieId);

            if (success)
            {
                return NoContent(); // 204 No Content indicates successful deletion
            }

            return NotFound("Bookmark not found."); // 404 Not Found if the bookmark doesn't exist
        }

    }

}
