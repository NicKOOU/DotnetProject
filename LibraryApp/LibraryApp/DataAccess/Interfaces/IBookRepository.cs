using LibraryApp.DataAccess.EfModels;
using LibraryApp.Dbo;

namespace LibraryApp.DataAccess.Interfaces
{
    public interface IBookRepository : DataAccess.IRepository<TBook, Book>
    {
        Task<Book> FindAsync(long id);
    }
}
