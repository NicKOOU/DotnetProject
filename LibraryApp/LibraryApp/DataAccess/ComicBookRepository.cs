using AutoMapper;
using LibraryApp.DataAccess.EfModels;
using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Dbo;

namespace LibraryApp.DataAccess
{
    public class ComicBookRepository : Repository<TComicsBook, ComicsBook>, IComicsBookRepository
    {
        public ComicBookRepository(LibraryAppContext context, ILogger<ComicBookRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {

        }

        public async Task<ComicsBook> FindAsync(long Id)
        {
            var result = await _context.ComicsBook.FindAsync(Id);
            return _mapper.Map<ComicsBook>(result);
        }
    }
}
