using LibraryApp.DataAccess.EfModels;
using LibraryApp.Dbo;

namespace LibraryApp.DataAccess.Interfaces
{
    public interface IComicsBookRepository : DataAccess.IRepository<TComicsBook, ComicsBook>
    {
        Task<ComicsBook> FindAsync(long id);
    }
}
