using BookBarn.DTOs;
using BookBarn.Models;
using BookBarn.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookBarn.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly BookBarnContext _context;

        public AuthorService(BookBarnContext context)
        {
            _context = context;
        }

        //public async Task<List<BookListItemDto>> GetAllAsync()
        //{
        //    // Business rule example:
        //    // Only return books with a title and a non-negative price.
        //    // Also enforce a consistent sort order.
        //    return await _context.Authors
        //        .AsNoTracking()
        //        .Where(b => b.Id != null && b.MiddleName.Trim() != "" )
        //        .OrderBy(b => b.Id)
        //        .Select(b => new AuthorListItemDto
        //        {
        //            Id = b.Id,
        //            Title = b.MiddleName!,
        //            Author = b.FirstName,
        //            Price = b.LastName
        //        })
        //        .ToListAsync();
        //}

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

        Task<List<AuthorListItemDto>> IAuthorService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<AuthorListItemDto?> IAuthorService.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

