using System.ComponentModel.DataAnnotations;

namespace BookBarn.Models
{
    public class BookSuggestionModel
    {
        [Required]
        [StringLength(80)]
        public string Title { get; set; }

        [Required]
        [StringLength(60)]
        public string Author { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(300)]
        public string Notes { get; set; }
    }
}