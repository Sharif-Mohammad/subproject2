using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Movies.Search
{
    public class MovieSimpleSearchRequest
    {
        public string UserId { get; set; }
        public string Query { get; set; }

        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
