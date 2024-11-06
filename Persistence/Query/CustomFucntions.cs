namespace Persistence.Query
{
    public static class CustomFucntions
    {
        private static string SelectQuery = "SELECT * From {0}.{1}";
        public const string GetUserBookmarks = "get_user_bookmarks(@p_user_id)";
        public const string GetMoviesStringSearch = "string_search(@p_user_id, @p_search_string, @p_offset, @p_limit)";
        public const string GetMoviesStructuredStringSearch = "structured_string_search(@p_title, @p_plot,@p_characters,@p_person_names,@p_user_id, @p_offset, @p_limit)";
        public const string FindMovieTitles = "find_movie_title(@p_title, @p_offset, @p_limit)";
        public const string FindCoPlayers = "find_co_players(@p_actor_name, @p_offset, @p_limit)";
        public const string GetActorsByPopularityAsync = "get_actors_by_popularity(@p_movie_id, @p_offset, @p_limit)";
        public const string GetSimilarMoviesAsync = "get_similar_movies(@p_movie_id, @p_offset, @p_limit)";
        public const string ExactMatchAsync = "exact_match_query(@p_keywords, @p_offset, @p_limit)";
        public const string UpdateNameRatings = "update_name_ratings()";
        public const string Rate = "rate(@p_user_id , @p_movie_id ,@p_rating ,@p_review )";
        public const string GetNameRatings = "get_name_rating(@p_name, @p_offset, @p_limit)";

        public static string GetFormattedSelectQuery(string schema, string functionName)
        {
            return string.Format(SelectQuery, schema, functionName);
        }
    }
}
