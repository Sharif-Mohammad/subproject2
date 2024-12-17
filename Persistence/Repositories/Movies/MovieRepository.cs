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

    public async Task<int> GetCountAsync()
    {
        return await context.Movies.CountAsync();
    }

    public async Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre)
    {
        return await context.Movies.Include(m => m.Genres).ThenInclude(g => g.Genre)
            .Where(m => m.Genres.Any(g => g.Genre.Name == genre)).AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetMoviesByIdsAsync(params string[]  moviesId)
    {
        return await context.Movies.Where(x=> moviesId.Contains(x.MovieId)).AsNoTracking()
            .ToListAsync();
    }

    public async Task<(IList<Movie>, int total)> SearchAsync(string title = null, string plot = null, int? releaseYear = null, bool? isAdult = null, int page = 1, int pageSize = 10)
    {
        var query = context.Movies.AsQueryable();

        // Apply search filters based on provided parameters
        if (!string.IsNullOrEmpty(title))
        {
            query = query.Where(m => EF.Functions.ILike(m.Title, $"%{title}%"));
        }

        if (!string.IsNullOrEmpty(plot))
        {
            query = query.Where(m => m.Plot != null && EF.Functions.ILike(m.Plot, $"%{plot}%"));
        }

        if (releaseYear.HasValue)
        {
            query = query.Where(m => m.ReleaseYear == releaseYear);
        }

        if (isAdult.HasValue)
        {
            query = query.Where(m => m.IsAdult == isAdult);
        }

        // Total count of matching movies (before pagination)
        int total = await query.CountAsync();

        // Apply pagination
        var movies = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        // Return the movies and the total count
        return (movies, total);
    }
}