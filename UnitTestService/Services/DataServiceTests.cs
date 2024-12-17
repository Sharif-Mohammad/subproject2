using Business.Services.Movies;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestService.Services
{
    public class DataServiceTests
    {

        private readonly SearchService search;
        private readonly ApplicationDbContext appDbContext;
        // Define your connection string
        string connectionString = "Host=localhost;Database=movie_dev;Username=Sabbir;Password=123456";

        public DataServiceTests()
        {
            // Set up the DbContextOptions with the connection string
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            //// Instantiate the DbContexta
            appDbContext = new ApplicationDbContext(options);

            search = new SearchService(appDbContext);
        }

        [Fact]
        public void ValidateCoplayersSearchResult()
        {
            var coPlayerResults = this.search.FindCoPlayers(new Business.Models.Movies.Search.CoPlayerSearchRequest
            {
                ActorName = "Tom hardy",
                Limit = 10,
                Offset = 0,
            }).Result;
            var firstPerson = coPlayerResults.FirstOrDefault();
            Assert.Equal("Wally Pfister", firstPerson.PersonName);
            Assert.Equal("nm0002892", firstPerson.PersonId);
            Assert.Equal(2, firstPerson.Frequency);
        }


        [Fact]
        public void ValidateMoviesSearchResult()
        {
            var results = this.search.FindMovieTitles(new Business.Models.Movies.Search.MovieSearchRequest
            {
                Limit = 10,
                Offset = 0,
                Title = "Harry potter"
            }).Result;
            var firstResult = results.FirstOrDefault();
            Assert.Equal("tt0304141", firstResult.MovieId);
            Assert.Equal("Harry Potter and the Prisoner of Azkaban", firstResult.PrimaryTitle);
        }


        [Fact]
        public void ValidateStringSearchResult()
        {
            var results = this.search.StringSearchAsync(new Business.Services.Movies.Search.MovieSimpleSearchRequest
            {
                Limit = 10,
                Offset = 0,
                Query = "Harry potter",
                UserId = "123"
            }).Result;
            var firstResult = results.FirstOrDefault();
            Assert.Equal("tt0304141", firstResult.MovieId);
            Assert.Equal("Harry Potter and the Prisoner of Azkaban", firstResult.PrimaryTitle);
        }

        [Fact]
        public void ValidateStructuredStringSearchResult()
        {
            var results = this.search.StructuredStringSearchAsync(new Business.Models.Movies.Search.StructuredStringSearchRequest
            {
                UserId = "123",
                Limit = 10,
                Offset = 0,
                Title = "Harry potter"
            }).Result;
            var firstResult = results.FirstOrDefault();
            Assert.Equal("tt0304141", firstResult.MovieId);
            Assert.Equal("Harry Potter and the Prisoner of Azkaban", firstResult.PrimaryTitle);
        }


        [Fact]
        public void ValidateGetActorsByPopularityAsyncSearchResult()
        {
            var results = this.search.GetActorsByPopularityAsync(new Business.Models.Movies.Search.ActorPopularitySearchRequest
            {
                Limit = 10,
                Offset = 0,
                MovieId = "tt0304141",
            }).Result;
            var firstResult = results.FirstOrDefault();
            Assert.Equal("nm0274913", firstResult.Id);
        }


        [Fact]
        public void ValidateExactMatchAsyncSearchResult()
        {
            var results = this.search.ExactMatchAsync(new Business.Models.Movies.Search.ExactMatchRequest
            {
                Keywords = new[] { "action" },
                Limit = 10,
                Offset = 0,

            }).Result;
            var firstResult = results.FirstOrDefault();
            Assert.Equal("tt0185906", firstResult.MovieId);
            Assert.Equal("Band of Brothers", firstResult.PrimaryTitle);
        }


    }
}

