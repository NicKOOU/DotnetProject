using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp.Pages
{
    public class CreateMovieModel : PageModel
    {
        private readonly ILogger<CreateMovieModel> _logger;
        private readonly IMovieRepository _movieRepository;

        public CreateMovieModel(ILogger<CreateMovieModel> logger, IMovieRepository movieRepository)
        {
            _logger = logger;
            _movieRepository = movieRepository;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _movieRepository.Insert(Movie);

            return RedirectToPage("Index");
        }
    }
}
