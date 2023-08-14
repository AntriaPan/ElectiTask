using Microsoft.EntityFrameworkCore;  // Import Entity Framework Core

namespace ElectiTask.Models
{
    // Define the DbContext class for user data
    public class UserContext : DbContext
    {
        // Constructor for UserContext that accepts DbContextOptions
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            // Constructor chaining
        }

        // DbSet for the User entity, representing a table in the database
        public DbSet<User> UserList { get; set; } = null!;

    }
}
