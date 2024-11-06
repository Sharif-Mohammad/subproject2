using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Frameworks
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string MovieId { get; set; }
        public string ActorId { get; set; }
        public string NoteContent { get; set; }
    }
}
