using Business.Models.Common;
using Business.Models.Frameworks;
using Business.Models.Movies;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Frameworks
{
    public class MovieRatings : IMovieRatings
    {
        private readonly IUnitOfWork _Repository;

        public MovieRatings(IUnitOfWork bookmarkRepository)
        {
            _Repository = bookmarkRepository;
        }
        public async Task<RatingsDto> Get(string movieId)
        {
            var ratings = await _Repository.Ratings.GetAllAsync(x => x.MovieId == movieId);

            if (ratings.Count() > 0)
            {
                var rating = ratings.FirstOrDefault();
                return new RatingsDto
                {
                    AverageRating = rating.AverageRating,
                    MovieId = movieId,
                    NumVotes = rating.NumVotes,
                };
            }

            return new RatingsDto();

        }

        public async Task<UserRatingsDto> GetUserRatings(string userId, string movieId)
        {
            var ratings = await _Repository.UserRatings.GetAllAsync(x => x.MovieId == movieId && x.UserId == userId);

            if (ratings.Count() > 0)
            {
                var rating = ratings.FirstOrDefault();
                return new UserRatingsDto
                {
                    Rating = rating.RatingValue,
                    MovieId = movieId,
                };
            }

            return new UserRatingsDto();

        }
    }
}
