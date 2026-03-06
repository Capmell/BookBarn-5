using BookBarn.DTOs;
using BookBarn.Models;
using BookBarn.Services.Interfaces;
using BookBarn.Services.Policies;
using Microsoft.EntityFrameworkCore;

namespace BookBarn.Services
{
    public class BookService : IBookService
    {

        private readonly IBookVisibilityPolicy _policy;
        private readonly BookBarnContext _context;
        private DefaultBookVisibilityPolicy defaultBookVisibilityPolicy;

        public BookService(BookBarnContext context)
        : this(context, new DefaultBookVisibilityPolicy())
        {
        }

        public BookService(BookBarnContext context, IBookVisibilityPolicy policy)
        {
            _context = context;
            _policy = policy;
        }
        public BookService(BookBarnContext context, DefaultBookVisibilityPolicy defaultBookVisibilityPolicy)
        {
            _context = context;
            this.defaultBookVisibilityPolicy = defaultBookVisibilityPolicy;
        }

        public async Task<List<BookListItemDto>> GetAllAsync()
        {
            return await _context.Books
                .AsNoTracking()
                .Where(b => _policy.IsVisible(b.Title, b.Price))
                .OrderBy(b => b.Title)
                .Select(b => new BookListItemDto
                {
                    Id = b.Id,
                    Title = b.Title!,
                    Author = b.Author,
                    Price = b.Price
                })
                .ToListAsync();
        }
        public async Task<BookListItemDto?> GetByIdAsync(int id)
        {
            return await _context.Books
                .AsNoTracking()
                .Where(b => b.Id == id)
                .Select(b => new BookListItemDto
                {
                    Id = b.Id,
                    Title = b.Title!,
                    Author = b.Author,
                    Price = b.Price
                })
                .FirstOrDefaultAsync();
        }
    }
}