using LibraryApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MyDbContext _dbContext;

        public IndexModel(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Book[] Books { get; private set; }

        public IActionResult OnGet()
        {
            Books = _dbContext.Books.ToArray();
            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
