using BookBarn.Services.Interfaces;
using BookBarn.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBarn.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllAsync();

            var vm = new BookListViewModel
            {
               
                PageTitle = "Available Books",
                TotalCount = books.Count,
                EmptyMessage = "No books are currently available."
            };

            return View(vm);
        }

        [Route("Books/Info")]
        public IActionResult About()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Manage()
        {
            return View();
        }
    }
}