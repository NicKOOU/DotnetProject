using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryApp.Pages
{
    public class CreateBookModel : PageModel
    {
        private readonly MyDbContext _dbContext;

        public CreateBookModel(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnPost(string title, string author, bool isread)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var book = new Book
            {
                Title = title,
                Author = author,
                IsRead = isread
            };

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
