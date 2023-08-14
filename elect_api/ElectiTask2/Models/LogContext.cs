using Microsoft.EntityFrameworkCore;  // Import Entity Framework Core

namespace ElectiTask.Models
{
    // Define the DbContext class for log data
    public class LogContext : DbContext
    {
        // Constructor for LogContext that accepts DbContextOptions
        public LogContext(DbContextOptions<LogContext> options) : base(options)
        {
            // Constructor chaining
        }

        // DbSet for the Log entity, representing a table in the database
        public DbSet<Log> log { get; set; } = null!;
    }
}
