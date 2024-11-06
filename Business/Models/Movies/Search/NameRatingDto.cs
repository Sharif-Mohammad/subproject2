using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Movies.Search
{
    public class NameRatingDto
    {
        public string PersonId { get; set; }  // ID of the person
        public string PersonName { get; set; }  // Name of the person
        public decimal WeightedRating { get; set; }  // The weighted rating
        public long TotalVotes { get; set; }  // Total number of votes
    }
}
