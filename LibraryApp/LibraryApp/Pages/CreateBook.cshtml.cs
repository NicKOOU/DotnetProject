using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;

namespace LibraryApp.Pages
{
    public class CreateBookModel : PageModel
    {
        private readonly MyDbContext _dbContext;

        public CreateBookModel(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Book Book { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _dbContext.Books.Add(Book);
            _dbContext.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
