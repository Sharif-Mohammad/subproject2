using Business.Models.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ApiTests
{
    public class NotesControllerTests : AuthControllerTestsBase
    {
        private const string NotesApi = $"{BaseApi}/api/notes";
        private const string TestMovieId = "tt0304141";
        private const string TestActorId = "nm0000199";

        [Fact]
        public async Task AddNoteToMovie_ValidRequest_Created()
        {
            var token = await GetAuthToken();
            var userId = await GetUserIdFromToken(token);

            var newNote = new NoteDto
            {
                MovieId = TestMovieId,
                ActorId = TestActorId,
                UserId = userId,
                NoteContent = "Movie was great"
            };

            var (response, statusCode) = await PostDataWithAuth($"{NotesApi}/users/{userId}/movies/{TestMovieId}", newNote, token);

            Assert.Equal(HttpStatusCode.Created, statusCode);
            Assert.Equal(userId, response?.Value("userId"));
            Assert.Equal(TestMovieId, response?.Value("movieId"));
        }

        [Fact]
        public async Task AddNoteToActor_ValidRequest_Created()
        {
            var token = await GetAuthToken();
            var userId = await GetUserIdFromToken(token);

            var newNote = new NoteDto
            {
                ActorId = TestActorId,
                UserId = userId,
                NoteContent = "Actor was great",
                MovieId = TestMovieId,
            };

            var (response, statusCode) = await PostDataWithAuth($"{NotesApi}/users/{userId}/actors/{TestActorId}", newNote, token);

            Assert.Equal(HttpStatusCode.Created, statusCode);
            Assert.Equal(userId, response?.Value("userId"));
            Assert.Equal(TestActorId, response?.Value("actorId"));
        }

        [Fact]
        public async Task GetNote_ValidNoteId_Ok()
        {
            var token = await GetAuthToken();
            var noteId = await CreateSampleNoteForMovie(token);

            var (response, statusCode) = await GetData($"{NotesApi}/{noteId}", token);

            int.TryParse(response?.Value("id"), out var id);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(noteId, id);
        }

        [Fact]
        public async Task GetNotesForUser_ValidUserId_Ok()
        {
            var token = await GetAuthToken();
            var userId = await GetUserIdFromToken(token);
            await CreateSampleNoteForMovie(token);  // Ensure at least one note exists for user

            var (response, statusCode) = await GetArray($"{NotesApi}/users/{userId}", token);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.NotEmpty(response); // Assumes user has at least one note
        }

        [Fact]
        public async Task UpdateNote_ValidRequest_Ok()
        {
            var token = await GetAuthToken();
            var noteId = await CreateSampleNoteForMovie(token);
            var userId = await GetUserIdFromToken(token);

            var updatedNote = new NoteDto
            {
                MovieId = TestMovieId,
                NoteContent = "Movie note Updated.",
                UserId = userId,
                ActorId = TestActorId,

            }; ;

            var (response, statusCode) = await PutData($"{NotesApi}/{noteId}", updatedNote, token);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal("Movie note Updated.", response?.Value("noteContent"));

        }

        [Fact]
        public async Task DeleteNote_ValidNoteId_NoContent()
        {
            var token = await GetAuthToken();
            var noteId = await CreateSampleNoteForMovie(token);

            var statusCode = await DeleteData($"{NotesApi}/{noteId}", token);

            Assert.Equal(HttpStatusCode.NoContent, statusCode);
        }

        // Helper methods

        private async Task<int> CreateSampleNoteForMovie(string token)
        {
            var userId = await GetUserIdFromToken(token);

            var newNote = new NoteDto
            {
                MovieId = TestMovieId,
                UserId = userId,
                NoteContent = "Movie was great",
                ActorId = TestActorId,
            };

            var (response, _) = await PostDataWithAuth($"{NotesApi}/users/{userId}/movies/{TestMovieId}", newNote, token);
            return int.Parse(response?.Value("id") ?? "0");
        }




    }

}
