using BookBarn.DTOs;

namespace BookBarn.Services.Interfaces
{
    public interface IBookService
    {
        Task<List<BookListItemDto>> GetAllAsync();
        Task<BookListItemDto?> GetByIdAsync(int id);
    }
}