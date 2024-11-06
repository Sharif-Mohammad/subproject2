using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Movies.Search
{
    public class MovieSearchResultDto
    {
        public string MovieId { get; set; } // Will map to 'movie_id' in the query
        public string PrimaryTitle { get; set; } // Will map to 'primarytitle' in the query
    }
}
