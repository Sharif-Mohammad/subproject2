using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Movies;

[Table("movies", Schema = DatabaseSchema.MovieSchema)]
public class Movie
{
    [Key]
    [Column("movie_id")]
    [StringLength(15)]
    public string MovieId { get; set; }

    [Required]
    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; }

    [Column("category")]
    [StringLength(255)]
    public string Category { get; set; }

    [Column("release_year")] public int? ReleaseYear { get; set; }

    [Column("runtime_minutes")] public int? RuntimeMinutes { get; set; }

    [Column("plot")] public string? Plot { get; set; }

    [Column("is_adult")] public bool IsAdult { get; set; } = false;

    [Column("poster_url")]
    [StringLength(255)]
    public string? PosterUrl { get; set; }

    [Column("box_office")]
    [StringLength(50)]
    public string? BoxOffice { get; set; }

    [Column("production")]
    [StringLength(100)]
    public string? Production { get; set; }

    [Column("created_at")] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<MovieCast> Cast { get; set; }
    public ICollection<MovieCrew> Crew { get; set; }
    public ICollection<MovieGenre> Genres { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<MovieSource> Sources { get; set; }
}