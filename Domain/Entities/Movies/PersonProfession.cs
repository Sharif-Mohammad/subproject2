using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Movies;

[Table("person_professions", Schema = DatabaseSchema.MovieSchema)]
public class PersonProfession
{
    [Key]
    [Column("person_id")]
    public string PersonId { get; set; }

    [Key]
    [Column("profession_id")]
    public int ProfessionId { get; set; }

    public Person Person { get; set; }
    public Profession Profession { get; set; }
}