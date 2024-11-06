using Business.Models.Movies;
using Business.Models.Movies.Search;
using Business.Services.Movies.Search;
using Domain;
using Persistence.Contexts;
using Persistence.Query;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Movies
{

    public class NameRatingsService : QuerySearch, INameRatingsService
    {
        public NameRatingsService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task UpdateNameRatings()
        {

            var sql = CustomFucntions.GetFormattedSelectQuery(DatabaseSchema.FrameworkSchema, CustomFucntions.UpdateNameRatings);

             await ExecuteNonQueryAsync(sql);

        }

        public async Task Rate(RateMovieRequest request)
        {

            var parameters = new
            {
                p_user_id = request.UserId,
                p_movie_id = request.MovieId,
                p_rating = request.Rating,
                p_review = request.Review
            };
            var sql = CustomFucntions.GetFormattedSelectQuery(DatabaseSchema.FrameworkSchema, CustomFucntions.Rate);

             await ExecuteNonQueryAsync(sql, parameters);

        }


        public async Task GetNameRatings(RateMovieRequest request)
        {

            var parameters = new
            {
                p_user_id = request.UserId,
                p_movie_id = request.MovieId,
                p_rating = request.Rating,
                p_review = request.Review
            };
            var sql = CustomFucntions.GetFormattedSelectQuery(DatabaseSchema.FrameworkSchema, CustomFucntions.Rate);

            await ExecuteNonQueryAsync(sql, parameters);

        }

        public async Task<IEnumerable<NameRatingDto>> GetNameRatingAsync(NameRatingSearchRequest searchRequest)
        {

            var sql = CustomFucntions.GetFormattedSelectQuery(DatabaseSchema.FrameworkSchema, CustomFucntions.GetNameRatings);
            var parameters = new { p_name = searchRequest.Name, p_offset = searchRequest.Offset, p_limit = searchRequest.Limit };

            return await ExecuteRawSqlAsync<NameRatingDto>(sql, parameters);

        }



    }
}
