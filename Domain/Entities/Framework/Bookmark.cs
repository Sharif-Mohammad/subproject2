using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Auth;

namespace Domain.Framework;

[Table("bookmarks", Schema = DatabaseSchema.FrameworkSchema)]
public class Bookmark
{

    [Key, Column("user_id", Order = 1)]
    public string UserId { get; set; }

    [Key, Column("movie_id", Order = 2)]
    [StringLength(15)]
    public string MovieId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("user_id")]
    public ApplicationUser User { get; set; }
}
