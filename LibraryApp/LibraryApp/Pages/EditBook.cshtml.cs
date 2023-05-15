using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibraryApp.Pages
{
    public class EditBookModel : PageModel
    {
        private readonly MyDbContext _dbContext;

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public Book Book { get; set; }

        [BindProperty]
        public bool IsRead { get; set; }

        public EditBookModel(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Book = await _dbContext.Books.FindAsync(id);

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

            var existingBook = await _dbContext.Books.FindAsync(Id); // Use the stored ID

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
                _dbContext.Entry(existingBook).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
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

        private bool BookExists(int id)
        {
            return _dbContext.Books.Any(e => e.Id == id);
        }
    }
}
