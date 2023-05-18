using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp.Pages
{
    public class ComicBook : PageModel
    {
        private readonly ILogger<ComicBook> _logger;
        private readonly IComicsBookRepository _comicsBookRepository;

        [BindProperty(SupportsGet = true)]
        public string TitleFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TypeFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? YearFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AuthorFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ReadFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ColorFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string BrandFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string LanguageFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        public List<ComicsBook> comicsBooks { get; set; }

        public ComicBook(ILogger<ComicBook> logger, IComicsBookRepository comicsBookRepository)
        {
            _logger = logger;
            _comicsBookRepository = comicsBookRepository;
            comicsBooks = new List<ComicsBook>();
        }

        public async Task<IActionResult> OnGet()
        {
            List<ComicsBook> comics = _comicsBookRepository.Get().Result.ToList();

            // Apply filters
            if (!string.IsNullOrEmpty(TitleFilter))
                comics = comics.FindAll(b => b.Title.Contains(TitleFilter));

            if (!string.IsNullOrEmpty(TypeFilter))
                comics = comics.FindAll(b => b.Type == TypeFilter);

            if (YearFilter.HasValue)
                comics = comics.FindAll(b => b.Year == YearFilter);

            if (!string.IsNullOrEmpty(AuthorFilter))
                comics = comics.FindAll(b => b.Author.Contains(AuthorFilter));

            if (!string.IsNullOrEmpty(BrandFilter))
                comics = comics.FindAll(b => b.Brand.Contains(BrandFilter));

            if (!string.IsNullOrEmpty(ReadFilter))
            {
                if (ReadFilter == "read")
                {
                    comics = comics.FindAll(b => b.IsRead);
                }
                else if (ReadFilter == "not-read")
                {
                    comics = comics.FindAll(b => !b.IsRead);
                }
            }
            if (!string.IsNullOrEmpty(ColorFilter))
            {
                if (ColorFilter == "color")
                {
                    comics = comics.FindAll(b => b.IsColor);
                }
                else if (ColorFilter == "no-color")
                {
                    comics = comics.FindAll(b => !b.IsColor);
                }
            }
            if (!string.IsNullOrEmpty(LanguageFilter))
                comics = comics.FindAll(b => b.Language == LanguageFilter);

            comicsBooks = comics;

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _comicsBookRepository.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            await _comicsBookRepository.Delete(book.Id);

            return RedirectToPage();
        }
    }
}
