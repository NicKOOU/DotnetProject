using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Pages
{
    public class EditComicBookModel : PageModel
    {
        private readonly ILogger<EditComicBookModel> _logger;
        private readonly IComicsBookRepository _comicsBookRepository;

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public ComicsBook ComicsBook { get; set; }

        [BindProperty]
        public bool IsRead { get; set; }

        [BindProperty]
        public bool IsColor { get; set; }

        public EditComicBookModel(ILogger<EditComicBookModel> logger, IComicsBookRepository comicsBookRepository)
        {
            _logger = logger;
            _comicsBookRepository = comicsBookRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ComicsBook = await _comicsBookRepository.FindAsync(id);

            if (ComicsBook == null)
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

            var existingComics = await _comicsBookRepository.FindAsync(Id); // Use the stored ID

            if (existingComics == null)
            {
                return NotFound();
            }

            // Update the properties of the existing book with the new values
            existingComics.Title = ComicsBook.Title;
            existingComics.Author = ComicsBook.Author;
            existingComics.Year = ComicsBook.Year;
            existingComics.Brand = ComicsBook.Brand;
            existingComics.IsRead = ComicsBook.IsRead;
            existingComics.IsColor = ComicsBook.IsColor;
            existingComics.Description = ComicsBook.Description;
            existingComics.Grade = ComicsBook.Grade;
            existingComics.Language = ComicsBook.Language;
            existingComics.PhotoUrl = ComicsBook.PhotoUrl;
            existingComics.Type = ComicsBook.Type;
            // Update other book properties here

            try
            {
                await _comicsBookRepository.Update(existingComics);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComicsExists(Id)) // Use the stored ID
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("ComicBook");
        }

        private bool ComicsExists(int id)
        {
            return _comicsBookRepository.FindAsync(id) != null;
        }
    }
}
