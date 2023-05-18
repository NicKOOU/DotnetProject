using LibraryApp.DataAccess.EfModels;
using LibraryApp.Dbo;

namespace LibraryApp.DataAccess.Interfaces
{
    public interface IMovieRepository : DataAccess.IRepository<TMovie, Movie>
    {
        Task<Movie> FindAsync(long id);
    }
}
