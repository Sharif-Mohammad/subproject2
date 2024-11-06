using Business.Models.Frameworks;
using Business.Models.Movies.Search;
using Business.Services.Movies.Search;
using Domain;
using Persistence.Contexts;
using Persistence.Query;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Movies
{
    public class SearchService : QuerySearch, ISearchService
    {
        public SearchService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MovieSearchResultDto>> StringSearchAsync(MovieSimpleSearchRequest searchRequest)
        {

            var sql = CustomFucntions.GetFormattedSelectQuery(DatabaseSchema.FrameworkSchema, CustomFucntions.GetMoviesStringSearch);
            var parameters = new { p_user_id = searchRequest.UserId, p_search_string = searchRequest.Query , p_offset =searchRequest.Offset, p_limit = searchRequest.Limit };

            return await ExecuteRawSqlAsync<MovieSearchResultDto>(sql, parameters);

        }

        public async Task<IEnumerable<MovieSearchResultDto>> StructuredStringSearchAsync(StructuredStringSearchRequest searchRequest)
        {
            var sql = CustomFucntions.GetFormattedSelectQuery(DatabaseSchema.FrameworkSchema, CustomFucntions.GetMoviesStructuredStringSearch);
            var parameters = new {
                p_title = searchRequest.Title,
                p_plot = searchRequest.Plot,
                p_characters = searchRequest.Characters,
                p_person_names = searchRequest.PersonNames,
                p_user_id = searchRequest.UserId,
                p_offset = searchRequest.Offset,
                p_limit = searchRequest.Limit };


            return await ExecuteRawSqlAsync<MovieSearchResultDto>(sql, parameters);
        }

        public async Task<IEnumerable<MovieSearchResultDto>> FindMovieTitles(MovieSearchRequest searchRequest)
        {

            var sql = CustomFucntions.GetFormattedSelectQuery(DatabaseSchema.FrameworkSchema, CustomFucntions.FindMovieTitles);
            var parameters = new { p_title = searchRequest.Title, p_offset = searchRequest.Offset, p_limit = searchRequest.Limit };

            return await ExecuteRawSqlAsync<MovieSearchResultDto>(sql, parameters);

        }

        public async Task<IEnumerable<CoPlayerResultDto>> FindCoPlayers(CoPlayerSearchRequest searchRequest)
        {

            var sql = CustomFucntions.GetFormattedSelectQuery(DatabaseSchema.FrameworkSchema, CustomFucntions.FindCoPlayers);
            var parameters = new { p_actor_name = searchRequest.ActorName, p_offset = searchRequest.Offset, p_limit = searchRequest.Limit };

            return await ExecuteRawSqlAsync<CoPlayerResultDto>(sql, parameters);

        }

        public async Task<IEnumerable<ActorPopularityResultDto>> GetActorsByPopularityAsync(ActorPopularitySearchRequest searchRequest)
        {

            var sql = CustomFucntions.GetFormattedSelectQuery(DatabaseSchema.FrameworkSchema, CustomFucntions.GetActorsByPopularityAsync);
            var parameters = new { p_movie_id = searchRequest.MovieId, p_offset = searchRequest.Offset, p_limit = searchRequest.Limit };

            return await ExecuteRawSqlAsync<ActorPopularityResultDto>(sql, parameters);

        }

        public async Task<IEnumerable<SimilarMovieResultDto>> GetSimilarMoviesAsync(SimilarMoviesRequest searchRequest)
        {

            var sql = CustomFucntions.GetFormattedSelectQuery(DatabaseSchema.FrameworkSchema, CustomFucntions.GetSimilarMoviesAsync);
            var parameters = new { p_movie_id = searchRequest.MovieId, p_offset = searchRequest.Offset, p_limit = searchRequest.Limit };

            return await ExecuteRawSqlAsync<SimilarMovieResultDto>(sql, parameters);

        }

        public async Task<IEnumerable<MovieSearchResultDto>> ExactMatchAsync(ExactMatchRequest searchRequest)
        {

            var sql = CustomFucntions.GetFormattedSelectQuery(DatabaseSchema.FrameworkSchema, CustomFucntions.ExactMatchAsync);
            var parameters = new { p_keywords = searchRequest.Keywords, p_offset = searchRequest.Offset, p_limit = searchRequest.Limit };

            return await ExecuteRawSqlAsync<MovieSearchResultDto>(sql, parameters);

        }

    }
}
