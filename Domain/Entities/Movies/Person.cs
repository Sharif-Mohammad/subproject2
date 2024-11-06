using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities.Movies;

[Table("people", Schema = DatabaseSchema.MovieSchema)]
public class Person
{
    [Key]
    [Column("person_id")]
    [StringLength(15)]
    public string PersonId { get; set; }

    [Required]
    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; }

    [Column("birth_year")]
    public int? BirthYear { get; set; }

    [Column("death_year")]
    public int? DeathYear { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<MovieCast> CastMovies { get; set; }
    public ICollection<MovieCrew> CrewMovies { get; set; }
    public ICollection<PersonProfession> Professions { get; set; }
}