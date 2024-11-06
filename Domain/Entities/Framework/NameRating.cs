using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Framework;

[Table("name_ratings", Schema = DatabaseSchema.FrameworkSchema)]
public class NameRating
{
    [Key]
    [Column("person_id")]
    [StringLength(15)]
    public string PersonId { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; }

    [Column("weighted_rating")]
    public decimal WeightedRating { get; set; }

    [Column("total_votes")]
    public long TotalVotes { get; set; }
}