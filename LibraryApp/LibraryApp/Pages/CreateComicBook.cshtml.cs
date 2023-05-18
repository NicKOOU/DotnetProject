using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp.Pages
{
    public class CreateComicBookModel : PageModel
    {
        private readonly ILogger<CreateComicBookModel> _logger;
        private readonly IComicsBookRepository _comicsBookRepository;

        public CreateComicBookModel(ILogger<CreateComicBookModel> logger, IComicsBookRepository comicsBookRepository)
        {
            _logger = logger;
            _comicsBookRepository = comicsBookRepository;
        }

        [BindProperty]
        public ComicsBook comicsBook { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _comicsBookRepository.Insert(comicsBook);

            return RedirectToPage("ComicBook");
        }
    }
}
