using Microsoft.EntityFrameworkCore;  // Import Entity Framework Core

namespace ElectiTask.Models
{
    // Define the DbContext class for book data
    public class BookContext : DbContext
    {
        // Constructor for BookContext that accepts DbContextOptions
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
            // Constructor chaining
        }

        // DbSet for the Book entity, representing a table in the database
        public DbSet<Book> BookList { get; set; } = null!;
    }
}
