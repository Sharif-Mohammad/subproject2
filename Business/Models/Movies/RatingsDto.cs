using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Movies
{
    public class RatingsDto
    {
        public string? MovieId { get; set; }
        public decimal? AverageRating { get; set; }

        public int? NumVotes { get; set; }
    }

    public class UserRatingsDto
    {
        public string? MovieId { get; set; }
        public decimal? Rating { get; set; }

    }
}
