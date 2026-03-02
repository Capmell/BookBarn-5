namespace BookBarn.Models
{
    public class Book
    {
        public int Id { get; set; }              // Primary key
        public string Title { get; set; } = "";  // Basic required text
        public string Author { get; set; } = "";
        public decimal Price { get; set; }
    }
}
