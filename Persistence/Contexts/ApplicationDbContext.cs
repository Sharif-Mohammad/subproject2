using Domain;
using Domain.Entities.Auth;
using Domain.Entities.Movies;
using Domain.Framework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using static Dapper.SqlMapper;

namespace Persistence.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    // DbSets for movie-related entities
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<MovieCast> MovieCast { get; set; }
    public DbSet<MovieCrew> MovieCrew { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<MovieSource> MovieSources { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<Profession> Professions { get; set; }
    public DbSet<PersonProfession> PersonProfessions { get; set; }
    public DbSet<Bookmark> Bookmarks { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Set the default schema for Identity tables
        builder.HasDefaultSchema(DatabaseSchema.FrameworkSchema);
        
        // Configure schema for movie-related tables
        builder.Entity<ApplicationUser>().ToTable("users", DatabaseSchema.FrameworkSchema);
        
        
         // Movies schema configuration
        MovieEnititesConfiguration(builder);
        
        // Users table mapping
        FrameworkEntitiesConfiguration(builder);
    }

    private static void FrameworkEntitiesConfiguration(ModelBuilder builder)
    {
        
        
        builder.Entity<UserRating>().ToTable("user_ratings", DatabaseSchema.FrameworkSchema);

        // Configure composite key
        builder.Entity<UserRating>().HasKey(r => new { r.RatingsId, r.UserId });
        
        
        
        builder.Entity<Bookmark>().ToTable("bookmarks", DatabaseSchema.FrameworkSchema);

        // Configure composite key
        builder.Entity<Bookmark>().HasKey(b => new { b.UserId, b.MovieId });
        
        builder.Entity<SearchHistory>().ToTable("search_history", DatabaseSchema.FrameworkSchema);

        // Configure composite key
        builder.Entity<SearchHistory>().HasKey(sh => new { sh.UserId, sh.SearchQuery, sh.CreatedAt });
        
        builder.Entity<Note>().ToTable("notes", DatabaseSchema.FrameworkSchema);

        builder.Entity<Note>().HasKey(n => new { n.NoteId });
        
        
        builder.Entity<NameRating>().ToTable("name_ratings", DatabaseSchema.FrameworkSchema);

        builder.Entity<NameRating>().HasKey(nr => nr.PersonId);
    }

    private static void MovieEnititesConfiguration(ModelBuilder builder)
    {
        builder.Entity<Movie>().ToTable("movies", DatabaseSchema.MovieSchema);
        builder.Entity<Genre>().ToTable("genres", DatabaseSchema.MovieSchema);
        builder.Entity<Person>().ToTable("people", DatabaseSchema.MovieSchema);
        builder.Entity<MovieCast>().ToTable("movie_cast", DatabaseSchema.MovieSchema);
        builder.Entity<MovieCrew>().ToTable("movie_crew", DatabaseSchema.MovieSchema);
        builder.Entity<Rating>().ToTable("ratings", DatabaseSchema.MovieSchema);
        builder.Entity<MovieSource>().ToTable("movie_sources", DatabaseSchema.MovieSchema);
        builder.Entity<MovieGenre>().ToTable("movie_genres", DatabaseSchema.MovieSchema);
        builder.Entity<Profession>().ToTable("professions", DatabaseSchema.MovieSchema);
        builder.Entity<PersonProfession>().ToTable("person_professions", DatabaseSchema.MovieSchema);


        // Configure the Rating entity
        builder.Entity<Rating>(entity =>
        {
            entity.HasKey(r => r.MovieId); // Specify MovieId as the primary key

            entity.HasOne(r => r.Movie)
                  .WithMany(m => m.Ratings)
                  .HasForeignKey(r => r.MovieId); // Use the same MovieId for the foreign key
        });
        // Configure composite keys for many-to-many relationships
        builder.Entity<MovieCast>()
            .HasKey(mc => new { mc.MovieId, mc.PersonId });


        builder.Entity<MovieCrew>()
            .HasKey(mc => new { mc.MovieId, mc.PersonId, mc.Role });

        builder.Entity<MovieGenre>()
            .HasKey(mg => new { mg.MovieId, mg.GenreId });

        builder.Entity<PersonProfession>()
            .HasKey(pp => new { pp.PersonId, pp.ProfessionId });
        
        // Configure composite primary key for MovieSource
        builder.Entity<MovieSource>()
            .HasKey(ms => new { ms.MovieId, ms.SourceName });

        // Relationship configurations
        builder.Entity<MovieCast>()
            .HasOne(mc => mc.Movie)
            .WithMany(m => m.Cast)
            .HasForeignKey(mc => mc.MovieId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.Entity<MovieCast>()
            .HasOne(mc => mc.Person)
            .WithMany(p => p.CastMovies)
            .HasForeignKey(mc => mc.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<MovieCrew>()
            .HasOne(mc => mc.Movie)
            .WithMany(m => m.Crew)
            .HasForeignKey(mc => mc.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<MovieCrew>()
            .HasOne(mc => mc.Person)
            .WithMany(p => p.CrewMovies)
            .HasForeignKey(mc => mc.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<MovieGenre>()
            .HasOne(mg => mg.Movie)
            .WithMany(m => m.Genres)
            .HasForeignKey(mg => mg.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<MovieGenre>()
            .HasOne(mg => mg.Genre)
            .WithMany(g => g.MovieGenres)
            .HasForeignKey(mg => mg.GenreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<PersonProfession>()
            .HasOne(pp => pp.Person)
            .WithMany(p => p.Professions)
            .HasForeignKey(pp => pp.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<PersonProfession>()
            .HasOne(pp => pp.Profession)
            .WithMany(p => p.PersonProfessions)
            .HasForeignKey(pp => pp.ProfessionId)
            .OnDelete(DeleteBehavior.Cascade);


        // Adding unique constraint to the 'name' column
        builder.Entity<Genre>().HasIndex(e => e.Name).IsUnique();
    }
}