using Domain.Entities.Movies;
using Domain.Framework;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Frameworks
{
    public class MovieUserRatingsRepository : BaseRepository<UserRating>, IMovieUserRatingsRepository
    {
        public MovieUserRatingsRepository(ApplicationDbContext context) : base(context) { }
    }
}
