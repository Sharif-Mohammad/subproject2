using Domain.Entities.Movies;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories.Movies;

// MovieRepository.cs
public class MovieRepository(ApplicationDbContext context) : IMovieRepository
{
    public async Task<Movie> GetByIdAsync(string id) => await context.Movies.FindAsync(id);

    public async Task<IEnumerable<Movie?>> GetAllAsync(int page, int pageSize)
    {
        return await context.Movies
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre)
    {
        return await context.Movies.Include(m => m.Genres).ThenInclude(g=>g.Genre)
            .Where(m => m.Genres.Any(g => g.Genre.Name == genre)).AsNoTracking()
            .ToListAsync();
    }
}