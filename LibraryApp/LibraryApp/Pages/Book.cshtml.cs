using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp.Pages
{
    public class BookModel : PageModel
    {
        private readonly ILogger<BookModel> _logger;
        private readonly IBookRepository _bookRepository;

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
        public string LanguageFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        public List<Book> Books { get; set; }

        public BookModel(ILogger<BookModel> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
            Books = new List<Book>();
        }

        public async Task<IActionResult> OnGet()
        {
            List<Book> books = _bookRepository.Get().Result.ToList();

            // Apply filters
            if (!string.IsNullOrEmpty(TitleFilter))
                books = books.FindAll(b => b.Title.Contains(TitleFilter));

            if (!string.IsNullOrEmpty(TypeFilter))
                books = books.FindAll(b => b.Type == TypeFilter);

            if (YearFilter.HasValue)
                books = books.FindAll(b => b.Year == YearFilter);

            if (!string.IsNullOrEmpty(AuthorFilter))
                books = books.FindAll(b => b.Author.Contains(AuthorFilter));

            if (!string.IsNullOrEmpty(ReadFilter))
            {
                if (ReadFilter == "read")
                {
                    books = books.FindAll(b => b.IsRead);
                }
                else if (ReadFilter == "not-read")
                {
                    books = books.FindAll(b => !b.IsRead);
                }
            }

            if (!string.IsNullOrEmpty(LanguageFilter))
                books = books.FindAll(b => b.Language == LanguageFilter);

            Books = books;

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _bookRepository.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            await _bookRepository.Delete(book.Id);

            return RedirectToPage();
        }
    }
}
