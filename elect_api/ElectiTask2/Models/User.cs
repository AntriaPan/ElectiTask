using System.ComponentModel.DataAnnotations;

namespace ElectiTask.Models
{
    // Define the User class
    public class User
    {
        // Properties that represent different attributes of a user
        [Key] // This attribute marks the property as a primary key
        [StringLength(50)] // Set the maximum length for the username
        public string? Username { get; set; }
        // A property to hold the username of the user (nullable string)
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
