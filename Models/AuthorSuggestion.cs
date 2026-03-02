using System.ComponentModel.DataAnnotations;

namespace BookBarn.Models
{
    public class AuthorSuggestion
    {
        [Required]
        [StringLength(60)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(80)]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(300)]
        public string Notes { get; set; }
    }
}
