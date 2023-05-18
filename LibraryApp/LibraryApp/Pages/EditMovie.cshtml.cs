using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Pages
{
    public class EditMovieModel : PageModel
    {
        private readonly ILogger<EditMovieModel> _logger;
        private readonly IMovieRepository _movieRepository;

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public Movie Movie { get; set; }

        [BindProperty]
        public bool IsWatched { get; set; }

        public EditMovieModel(ILogger<EditMovieModel> logger, IMovieRepository movieRepository)
        {
            _logger = logger;
            _movieRepository = movieRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Movie = await _movieRepository.FindAsync(id);

            if (Movie == null)
            {
                return NotFound();
            }

            Id = id; // Store the ID separately

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingMovie = await _movieRepository.FindAsync(Id); // Use the stored ID

            if (existingMovie == null)
            {
                return NotFound();
            }

            existingMovie.Title = Movie.Title;
            existingMovie.Year = Movie.Year;
            existingMovie.IsWatched = Movie.IsWatched;
            existingMovie.Description = Movie.Description;
            existingMovie.Grade = Movie.Grade;
            existingMovie.Language = Movie.Language;
            existingMovie.PosterUrl = Movie.PosterUrl;
            existingMovie.Genre = Movie.Genre;
            existingMovie.Duration = Movie.Duration;
            // Update other book properties here

            try
            {
                await _movieRepository.Update(existingMovie);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Id)) // Use the stored ID
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MovieExists(int id)
        {
            return _movieRepository.FindAsync(id) != null;
        }
    }
}
