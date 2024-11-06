using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Movies;

[Table("movie_crew", Schema = DatabaseSchema.MovieSchema)]
public class MovieCrew
{
    [Key]
    [Column("movie_id")]
    public string MovieId { get; set; }

    [Key]
    [Column("person_id")]
    public string PersonId { get; set; }

    [Key]
    [StringLength(50)]
    public string Role { get; set; }

    public Movie Movie { get; set; }
    public Person Person { get; set; }
}