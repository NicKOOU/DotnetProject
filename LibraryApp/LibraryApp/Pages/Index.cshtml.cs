using LibraryApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MyDbContext _dbContext;

        [BindProperty(SupportsGet = true)]
        public string TitleFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TypeFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? YearFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AuthorFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool? ReadFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string LanguageFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        public List<Book> Books { get; set; }

        public IndexModel(MyDbContext dbContext)
        {
            _dbContext = dbContext;
            Books = new List<Book>();
        }

        public async Task<IActionResult> OnGet()
        {
            var query = _dbContext.Books.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(TitleFilter))
                query = query.Where(b => b.Title.Contains(TitleFilter));

            if (!string.IsNullOrEmpty(TypeFilter))
                query = query.Where(b => b.Type == TypeFilter);

            if (YearFilter.HasValue)
                query = query.Where(b => b.Year == YearFilter);

            if (!string.IsNullOrEmpty(AuthorFilter))
                query = query.Where(b => b.Author.Contains(AuthorFilter));

            if (ReadFilter.HasValue)
                query = query.Where(b => b.IsRead == ReadFilter);

            if (!string.IsNullOrEmpty(LanguageFilter))
                query = query.Where(b => b.Language == LanguageFilter);

            // Sort by grade
            if (SortBy == "Grade")
                query = query.OrderByDescending(b => b.Grade);

            Books = await query.ToListAsync();

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
