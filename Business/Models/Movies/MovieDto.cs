namespace Business.Models.Movies;

public class MovieDto
{
    public string MovieId { get; set; }
    public string Title { get; set; }
    public int? ReleaseYear { get; set; }
    public int? RuntimeMinutes { get; set; }
    public bool IsAdult { get; set; }
    public string Plot { get; set; }
    public string SelfUrl { get; set; }
}