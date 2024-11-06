namespace Business.Models.Movies.Search
{
    public class SimilarMovieResultDto
    {
        public string SimilarMovieId { get; set; }
        public string SimilarMovieTitle { get; set; }
        public long SharedGenreCount { get; set; }
        public long SharedCastCount { get; set; }
    }

}
