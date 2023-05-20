using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.DataAccess.EfModels;
using LibraryApp.DataAccess.Interfaces;
using LibraryApp.Pages;
using LibraryApp.Dbo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryTests
{
    internal class BookPageTest
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
        public BookPageTest(ILogger<BookModel> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
            Books = new List<Book>();
        }

        //test add book
        [Fact]
        public async Task<IActionResult> OnPostAddBook()
        {
            Book book = new Book();
            book.Title = "Test Book";
            book.Author = "Test Author";
            book.Type = "Test Type";
            book.Year = 2021;
            book.IsRead = false;
            book.Language = "Test Language";
            await _bookRepository.Insert(book);
            return RedirectResult("Book");
        }




    }
}
