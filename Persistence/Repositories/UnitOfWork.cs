using Persistence.Contexts;
using Persistence.Repositories.Frameworks;
using Persistence.Repositories.Movies;

namespace Persistence.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;

    public IMovieRepository Movies { get; }
    public IBookmarkRepository Bookmarks { get; }
    public INotesRepository Notes { get; }

    public UnitOfWork(ApplicationDbContext context, IMovieRepository movieRepository, IBookmarkRepository bookmarkRepository, INotesRepository notes)
    {
        _context = context;
        Movies = movieRepository;
        Bookmarks = bookmarkRepository;
        Notes = notes;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}