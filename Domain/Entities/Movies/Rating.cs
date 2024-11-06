using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Movies;

[Table("ratings", Schema = DatabaseSchema.MovieSchema)]
public class Rating
{
    [Key]
    [Column("movie_id")]
    [ForeignKey("Movie")]
    public string MovieId { get; set; }

    [Column("averagerating", TypeName = "decimal(5,1)")]
    public decimal AverageRating { get; set; }

    [Column("numvotes")]
    public int NumVotes { get; set; }

    public virtual Movie Movie { get; set; }
}