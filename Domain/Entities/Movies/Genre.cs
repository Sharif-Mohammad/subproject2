using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Movies;

[Table("genres", Schema = DatabaseSchema.MovieSchema)]
public class Genre
{
    [Key]
    [Column("genre_id")]
    public int GenreId { get; set; }

    [Required]
    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; }

    public ICollection<MovieGenre> MovieGenres { get; set; }
}