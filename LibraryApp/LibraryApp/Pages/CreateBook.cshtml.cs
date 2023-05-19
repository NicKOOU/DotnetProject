using LibraryApp.DataAccess.EfModels;
using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Dbo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Drawing.Printing;
using System.IO;

namespace LibraryApp.Pages
{
    public class CreateBookModel : PageModel
    {
        private readonly ILogger<CreateBookModel> _logger;
        private readonly IBookRepository _bookRepository;

        public CreateBookModel(ILogger<CreateBookModel> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }

        [BindProperty]
        public Book Book { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _bookRepository.Insert(Book);

            return RedirectToPage("Book");
        }
    }
}
