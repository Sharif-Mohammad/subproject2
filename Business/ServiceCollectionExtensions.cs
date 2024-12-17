using Business.Services.Frameworks;
using Business.Services.Movies;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using Persistence.Repositories.Movies;

namespace Business;

public static class ServiceCollectionExtensions
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IBookmarkService, BookmarkService>();
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<ISearchService, SearchService>();
        services.AddScoped<INameRatingsService, NameRatingsService>();
        services.AddScoped<IMovieRatings, MovieRatings>();
    }
    }
