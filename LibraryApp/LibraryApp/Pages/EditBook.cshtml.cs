using LibraryApp.DataAccess.EfModels;
using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibraryApp.Pages
{
    public class EditBookModel : PageModel
    {
        private readonly ILogger<EditBookModel> _logger;
        private readonly IBookRepository _bookRepository;

        [BindProperty]
        public long Id { get; set; }

        [BindProperty]
        public Book Book { get; set; }

        [BindProperty]
        public bool IsRead { get; set; }

        public EditBookModel(ILogger<EditBookModel> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Book = await _bookRepository.FindAsync(id);

            if (Book == null)
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

            var existingBook = await _bookRepository.FindAsync(Id); // Use the stored ID

            if (existingBook == null)
            {
                return NotFound();
            }

            // Update the properties of the existing book with the new values
            existingBook.Title = Book.Title;
            existingBook.Author = Book.Author;
            existingBook.Year = Book.Year;
            existingBook.IsRead = Book.IsRead;
            existingBook.Description = Book.Description;
            existingBook.Grade = Book.Grade;
            existingBook.Language = Book.Language;
            existingBook.PhotoUrl = Book.PhotoUrl;
            existingBook.Type = Book.Type;
            // Update other book properties here

            try
            {
                await _bookRepository.Update(existingBook);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Id)) // Use the stored ID
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

        private bool BookExists(long id)
        {
            return _bookRepository.FindAsync(id) != null;
        }
    }
}
