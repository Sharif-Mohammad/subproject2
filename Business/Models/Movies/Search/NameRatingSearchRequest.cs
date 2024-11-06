namespace Business.Models.Movies.Search
{
    public class NameRatingSearchRequest
    {
        public string Name { get; set; }  // The partial or full name to search for
        public int Offset { get; set; } = 0;  // The starting point for pagination
        public int Limit { get; set; } = 10;  // Number of results per page
    }
}
