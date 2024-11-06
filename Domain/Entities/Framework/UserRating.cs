using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Auth;

namespace Domain.Framework;

[Table("user_ratings", Schema = DatabaseSchema.FrameworkSchema)]
public class UserRating
{
    [Key, Column("ratings_id", Order = 1)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RatingsId { get; set; }

    [Key, Column("user_id", Order = 2)]
    public string UserId { get; set; }

    [StringLength(15)]
    [Column("movie_id")]
    public string MovieId { get; set; }

    [Column("rating")]
    [Range(0, 10)]
    public decimal RatingValue { get; set; }

    [Column("review")]
    public string Review { get; set; }

    [Column("created_at")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [ForeignKey("user_id")]
    public ApplicationUser User { get; set; }
}