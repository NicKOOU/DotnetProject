using AutoMapper;
using LibraryApp.DataAccess.EfModels;
using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Dbo;

namespace LibraryApp.DataAccess
{
    public class BookRepository : Repository<TBook, Book>, IBookRepository
    {
        public BookRepository(LibraryAppContext context, ILogger<BookRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {

        }
        public async Task<Book> FindAsync(long Id)
        {
            var result = await _context.Books.FindAsync(Id);
            return _mapper.Map<Book>(result);
        }
    }
}
