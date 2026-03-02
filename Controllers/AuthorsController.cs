using BookBarn.Models;
using BookBarn.Services;
using BookBarn.Services.Interfaces;
using BookBarn.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBarn.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorservice)
        {
            _authorService = authorservice;
        }

        public async Task<IActionResult> Index()
        {
            var author = await _authorService.GetAllAsync();

            var vm = new BookListViewModel
            {

                PageTitle = "Available author",
                TotalCount = author.Count,
                EmptyMessage = "No authors are currently available."
            };

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
    }
}
