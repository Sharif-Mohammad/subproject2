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
        public async Task<IActionResult> GetBookmarksForUser(string userId)
        {
            var notes = await _bookmarkService.GetUserBookmarksAsync(userId);
            return Ok(notes);
        }
    }

}
