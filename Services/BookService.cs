using BookBarn.DTOs;
using BookBarn.Models;
using BookBarn.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookBarn.Services
{
    public class BookService : IBookService
    {
        private readonly BookBarnContext _context;

        public BookService(BookBarnContext context)
        {
            _context = context;
        }

        public async Task<List<BookListItemDto>> GetAllAsync()
        {
            // Business rule example:
            // Only return books with a title and a non-negative price.
            // Also enforce a consistent sort order.
            return await _context.Books
                .AsNoTracking()
                .Where(b => b.Title != null && b.Title.Trim() != "" && b.Price >= 0)
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