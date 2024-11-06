using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Movies.Search
{
    public class StructuredStringSearchRequest
    {
        public string? Title { get; set; }
        public string? Plot { get; set; }
        public string? Characters { get; set; }
        public string? PersonNames { get; set; }
        public string? UserId { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
