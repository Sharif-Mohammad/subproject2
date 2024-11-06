using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Movies.Search
{
    public class CoPlayerSearchRequest
    {
        public string ActorName { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
