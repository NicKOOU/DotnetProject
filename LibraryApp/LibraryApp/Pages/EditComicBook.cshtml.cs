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
        public ComicsBook comicsBook { get; set; }

        [BindProperty]
        public bool IsRead { get; set; }

        [BindProperty]
        public Book IsColor { get; set; }

        public EditComicBookModel(ILogger<EditComicBookModel> logger, IComicsBookRepository comicsBookRepository)
        {
            _logger = logger;
            _comicsBookRepository = comicsBookRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            comicsBook = await _comicsBookRepository.FindAsync(id);

            if (comicsBook == null)
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
            existingComics.Title = comicsBook.Title;
            existingComics.Author = comicsBook.Author;
            existingComics.Year = comicsBook.Year;
            existingComics.Brand = comicsBook.Brand;
            existingComics.IsRead = comicsBook.IsRead;
            existingComics.IsColor = comicsBook.IsColor;
            existingComics.Description = comicsBook.Description;
            existingComics.Grade = comicsBook.Grade;
            existingComics.Language = comicsBook.Language;
            existingComics.PhotoUrl = comicsBook.PhotoUrl;
            existingComics.Type = comicsBook.Type;
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
