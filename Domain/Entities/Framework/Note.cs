using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Auth;

namespace Domain.Framework;


[Table("notes", Schema = DatabaseSchema.FrameworkSchema)]
public class Note
{
    [Key]
    [Column("note_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NoteId { get; set; }

    [Column("user_id")]
    public string UserId { get; set; }

    [Column("movie_id")]
    [StringLength(15)]
    public string MovieId { get; set; }

    [Column("actor_id")]
    [StringLength(15)]
    public string ActorId { get; set; }

    [Required]
    [Column("note")]
    public string NoteContent { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [ForeignKey("user_id")]
    public ApplicationUser User { get; set; }
}