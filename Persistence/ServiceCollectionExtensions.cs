using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using Persistence.Repositories.Frameworks;
using Persistence.Repositories.Movies;

namespace Persistence;

public static class ServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IBookmarkRepository, BookmarkRepository>();
        services.AddScoped<INotesRepository, NotesRepository>();
    }

}