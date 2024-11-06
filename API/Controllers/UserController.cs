using Business.Services.Frameworks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/users/{userId}/bookmarks")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;

        public UserController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserBookmarks(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            var bookmarks = await _bookmarkService.GetUserBookmarksAsync(userId);

            if (bookmarks == null || !bookmarks.Any())
            {
                return NotFound("No bookmarks found for this user.");
            }

            return Ok(bookmarks);
        }
    }

}
