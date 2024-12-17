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
    public class MovieRatingsRepository : BaseRepository<Rating>, IMovieRatingsRepository
    {
        public MovieRatingsRepository(ApplicationDbContext context) : base(context) { }
    }
}
