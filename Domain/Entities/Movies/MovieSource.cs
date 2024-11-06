using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Movies;

[Table("movie_sources", Schema = DatabaseSchema.MovieSchema)]
public class MovieSource
{
    [Key]
    [Column("movie_id")]
    public string MovieId { get; set; }

    [Key]
    [StringLength(50)]
    [Column("source_name")]
    public string SourceName { get; set; }

    [Column("source_url")]
    [StringLength(255)]
    public string SourceUrl { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Movie Movie { get; set; }
}