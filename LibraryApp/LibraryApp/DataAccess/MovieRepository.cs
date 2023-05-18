using AutoMapper;
using LibraryApp.DataAccess.EfModels;
using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Dbo;

namespace LibraryApp.DataAccess
{
    public class MovieRepository : Repository<TMovie, Movie>, IMovieRepository
    {
        public MovieRepository(LibraryAppContext context, ILogger<MovieRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {

        }

        public async Task<Movie> FindAsync(long id)
        {
            var movie = await _context.Movies.FindAsync(id);
            return _mapper.Map<Movie>(movie);
        }
    }
}
