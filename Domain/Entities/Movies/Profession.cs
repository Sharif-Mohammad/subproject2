using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Movies;

[Table("professions", Schema = DatabaseSchema.MovieSchema)]
public class Profession
{
    [Key]
    [Column("profession_id")]
    public int ProfessionId { get; set; }

    [Required]
    [Column("profession_name")]
    [StringLength(100)]
    public string ProfessionName { get; set; }

    public ICollection<PersonProfession> PersonProfessions { get; set; }
}