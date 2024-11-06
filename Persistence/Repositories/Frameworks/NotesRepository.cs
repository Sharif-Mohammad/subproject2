using Domain.Framework;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Frameworks
{
    public class NotesRepository : BaseRepository<Note>,INotesRepository
    {
        public NotesRepository(ApplicationDbContext context) : base(context) { }
    }
}
