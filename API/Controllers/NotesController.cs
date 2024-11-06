using Business.Models.Frameworks;
using Business.Services.Frameworks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        // Add a new note for a movie
        [HttpPost("users/{userId}/movies/{movieId}")]
        public async Task<IActionResult> AddNoteToMovie(string userId, string movieId, [FromBody] NoteDto noteDto)
        {
            noteDto.UserId = userId;
            noteDto.MovieId = movieId;
            var note = await _noteService.AddNoteAsync(noteDto);
            return CreatedAtAction(nameof(GetNote), new { noteId = note.Id }, note);
        }

        // Add a new note for an actor
        [HttpPost("users/{userId}/actors/{actorId}")]
        public async Task<IActionResult> AddNoteToActor(string userId, string actorId, [FromBody] NoteDto noteDto)
        {
            noteDto.UserId = userId;
            noteDto.ActorId = actorId;
            var note = await _noteService.AddNoteAsync(noteDto);
            return CreatedAtAction(nameof(GetNote), new { noteId = note.Id }, note);
        }

        // Get a specific note by note ID
        [HttpGet("{noteId}")]
        public async Task<IActionResult> GetNote(int noteId)
        {
            var note = await _noteService.GetNoteByIdAsync(noteId);
            if (note == null) return NotFound();
            return Ok(note);
        }

        // Get all notes for a specific user
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetNotesForUser(string userId)
        {
            var notes = await _noteService.GetNotesByUserIdAsync(userId);
            return Ok(notes);
        }

        // Update a note
        [HttpPut("{noteId}")]
        public async Task<IActionResult> UpdateNote(int noteId, [FromBody] NoteDto noteDto)
        {
            var result = await _noteService.UpdateNoteAsync(noteId, noteDto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // Delete a note
        [HttpDelete("{noteId}")]
        public async Task<IActionResult> DeleteNote(int noteId)
        {
            var result = await _noteService.DeleteNoteAsync(noteId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
