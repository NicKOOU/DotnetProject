using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp.Pages
{
    public class MovieModel : PageModel
    {
        private readonly ILogger<MovieModel> _logger;
        private readonly IMovieRepository _movieRepository;

        [BindProperty(SupportsGet = true)]
        public string TitleFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string GenreFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? YearFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string WatchFilter { get; set; }



        [BindProperty(SupportsGet = true)]
        public string LanguageFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        public List<Movie> Movies { get; set; }

        public MovieModel(ILogger<MovieModel> logger, IMovieRepository movieRepository)
        {
            _logger = logger;
            _movieRepository = movieRepository;
            Movies = new List<Movie>();
        }

        public async Task<IActionResult> OnGet()
        {
            List<Movie> movies = _movieRepository.Get().Result.ToList();

            // Apply filters
            if (!string.IsNullOrEmpty(TitleFilter))
                movies = movies.FindAll(b => b.Title.Contains(TitleFilter));

            if (!string.IsNullOrEmpty(GenreFilter))
                movies = movies.FindAll(b => b.Genre == GenreFilter);

            if (YearFilter.HasValue)
                movies = movies.FindAll(b => b.Year == YearFilter);

            if (!string.IsNullOrEmpty(WatchFilter))
            {
                if (WatchFilter == "watch")
                {
                    movies = movies.FindAll(b => b.IsWatched);
                }
                else if (WatchFilter == "not-watch")
                {
                    movies = movies.FindAll(b => !b.IsWatched);
                }
            }

            if (!string.IsNullOrEmpty(LanguageFilter))
                movies = movies.FindAll(b => b.Language == LanguageFilter);

            Movies = movies;

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _movieRepository.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            await _movieRepository.Delete(book.Id);

            return RedirectToPage();
        }
    }
}
