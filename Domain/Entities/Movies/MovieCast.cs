using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Movies;

[Table("movie_cast", Schema = DatabaseSchema.MovieSchema)]
public class MovieCast
{
    [Key]
    [Column("movie_id")]
    public string MovieId { get; set; }

    [Key]
    [Column("person_id")]
    public string PersonId { get; set; }

    [Column("character")]
    [StringLength(255)]
    public string Character { get; set; }

    [Column("ordering")]
    public int Ordering { get; set; }

    public Movie Movie { get; set; }
    public Person Person { get; set; }
}