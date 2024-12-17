using Business.Exceptions;
using Business.Models.Frameworks;
using Domain.Framework;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Frameworks
{
    public class NoteService : INoteService
    {
        private readonly IUnitOfWork unitOfWork;

        public NoteService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<NoteDto> AddNoteAsync(NoteDto noteDto)
        {
            var note = MapToDb(noteDto);
            await this.unitOfWork.Notes.AddAsync(note);

            await unitOfWork.CompleteAsync();

            return MapToDto(note);
        }

        private static Note MapToDb(NoteDto noteDto)
        {
            return new Note
            {
                ActorId = noteDto.ActorId,
                MovieId = noteDto.MovieId,
                NoteContent = noteDto.NoteContent,
                UserId = noteDto.UserId,
                CreatedAt = DateTime.UtcNow
            };
        }

        private static NoteDto MapToDto(Note note)
        {
            return new NoteDto
            {
                ActorId = note.ActorId,
                MovieId = note.MovieId,
                NoteContent = note.NoteContent,
                UserId = note.UserId,
                Id = note.NoteId,
            };
        }

        public async Task<bool> DeleteNoteAsync(int noteId)
        {
            var note = await this.unitOfWork.Notes.GetByIdAsync(noteId);
            unitOfWork.Notes.Delete(note);
            return await unitOfWork.CompleteAsync() > 0;
        }

        public async Task<NoteDto> GetNoteByIdAsync(int noteId)
        {
            var note = await this.unitOfWork.Notes.GetByIdAsync(noteId);
            return note != null ? MapToDto(note) : throw new NotFoundException($"Note with ID {noteId} not found.");
        }

        public async Task<IEnumerable<NoteDto>> GetNotesByUserIdAsync(string userId)
        {
            var userNotes = await unitOfWork.Notes.GetAllAsync(
                         note => note.UserId == userId,
                         0,
                         int.MaxValue,
                         note => note.User
                     );

            return userNotes.Select(MapToDto);
        }

        public async Task<NoteDto> UpdateNoteAsync(int noteId, NoteDto noteDto)
        {
            var note = await this.unitOfWork.Notes.GetByIdAsync(noteId);
            if (note != null)
            {
                note.NoteContent = noteDto.NoteContent.Trim();

                note.MovieId = noteDto.MovieId;
                note.ActorId = noteDto.ActorId;
                
                this.unitOfWork.Notes.Update(note);
                await unitOfWork.CompleteAsync();

                return noteDto;
            }
            throw new NotFoundException($"Note with ID {noteId} not found.");
        }
    }
}
