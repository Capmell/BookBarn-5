using BookBarn.DTOs;

namespace BookBarn.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorListItemDto>> GetAllAsync();
        Task<AuthorListItemDto?> GetByIdAsync(int id);
    }
}
