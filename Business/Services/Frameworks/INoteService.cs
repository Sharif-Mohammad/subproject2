using Business.Models.Frameworks;
using Domain.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Frameworks
{
    public interface INoteService
    {
        // Adds a new note, either for a movie or an actor based on the provided NoteDto.
        Task<NoteDto> AddNoteAsync(NoteDto noteDto);

        // Retrieves a specific note by its ID.
        Task<NoteDto> GetNoteByIdAsync(int noteId);

        // Retrieves all notes created by a specific user.
        Task<IEnumerable<NoteDto>> GetNotesByUserIdAsync(string userId);

        // Updates an existing note by its ID.
        Task<NoteDto> UpdateNoteAsync(int noteId, NoteDto noteDto);

        // Deletes a note by its ID.
        Task<bool> DeleteNoteAsync(int noteId);
    }
}
