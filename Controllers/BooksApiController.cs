using BookBarn.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBarn.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksApiController : ControllerBase
    {
        private readonly IBookService _bookService;

        private readonly IAuthorService _authorService;

        public BooksApiController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public BooksApiController(IAuthorService authorservice)
        {
            _authorService = authorservice;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        //[Authorize]
        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var book = await _bookService.GetByIdAsync(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(book);
        //}

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }
    }
}