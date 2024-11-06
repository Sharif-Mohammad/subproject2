using Persistence.Repositories.Frameworks;
using Persistence.Repositories.Movies;

namespace Persistence.Repositories;

// IUnitOfWork.cs
public interface IUnitOfWork : IDisposable
{
    IMovieRepository Movies { get; }

    IBookmarkRepository Bookmarks { get; }
    INotesRepository Notes { get; }

    Task<int> CompleteAsync();  // Commit all changes
}
