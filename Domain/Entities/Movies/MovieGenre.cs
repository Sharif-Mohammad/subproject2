using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Movies;

[Table("movie_genres", Schema = DatabaseSchema.MovieSchema)]
public class MovieGenre
{
    [Key]
    [Column("movie_id")]
    public string MovieId { get; set; }

    [Key]
    [Column("genre_id")]
    public int GenreId { get; set; }

    public Movie Movie { get; set; }
    public Genre Genre { get; set; }
}