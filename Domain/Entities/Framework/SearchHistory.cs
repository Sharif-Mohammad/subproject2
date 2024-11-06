using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Auth;

namespace Domain.Framework;

[Table("search_history", Schema = DatabaseSchema.FrameworkSchema)]
public class SearchHistory
{
    [Key, Column("user_id", Order = 1)]
    public string UserId { get; set; }

    [Key, Column("search_query", Order = 2)]
    public string SearchQuery { get; set; }

    [Required]
    [Column("search_type")]
    [StringLength(50)]
    public string SearchType { get; set; }

    [Column("created_at")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [ForeignKey("user_id")]
    public ApplicationUser User { get; set; }
}
