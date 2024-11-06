using Domain.Framework;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Frameworks
{
    public class BookmarkRepository : BaseRepository<Bookmark>,IBookmarkRepository
    {
        public BookmarkRepository(ApplicationDbContext context) : base(context) { }
    }
}
