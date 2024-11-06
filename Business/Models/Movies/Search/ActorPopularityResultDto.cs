namespace Business.Models.Movies.Search
{
    public class ActorPopularityResultDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal WeightedRating { get; set; }
        public long TotalVotes { get; set; }
    }

}
