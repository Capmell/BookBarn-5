using System.Collections.Generic;
using BookBarn.DTOs;

namespace BookBarn.ViewModels
{
    public class BookListViewModel
    {
        public List<BookListItemDto> Books { get; set; } = new();
        public string PageTitle { get; set; } = "Books";
        public int TotalCount { get; set; }
        public string EmptyMessage { get; set; } = "No books found.";
    }
}