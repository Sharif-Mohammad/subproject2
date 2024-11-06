using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Movies
{
    public class RateMovieRequest
    {
        public string UserId { get; set; }      // ID of the user rating the movie
        public string MovieId { get; set; }   // ID of the movie (tconst)
        public int Rating { get; set; }        // Rating value (1 to 10)
        public string Review { get; set; }     // Optional review text
    }
}
