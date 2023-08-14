using System.ComponentModel.DataAnnotations;

namespace ElectiTask.Models
{
    // Define the Book class
    public class Book
    {
        // Properties that represent the attributes of a book
        public Guid? Id { get; set; }
        // A property to hold the unique identifier of the book
        [Required(ErrorMessage = "Title cannot be empty.")]
        public string? Title { get; set; }
        public string? Author { get; set; }
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Published year must be exactly 4 digits.")]
        public string? PublishedYear { get; set; }

        [RegularExpression(@"^\d{13}$", ErrorMessage = "ISBN must be exactly 13 digits.")]
        public string? ISBN { get; set; }
    }
}
