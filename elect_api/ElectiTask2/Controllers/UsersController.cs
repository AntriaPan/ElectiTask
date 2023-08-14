using ElectiTask.Models;  // Import necessary models
using Microsoft.AspNetCore.Mvc;  // Import ASP.NET Core MVC components
using Microsoft.EntityFrameworkCore;  // Import Entity Framework components
using NLog;  // Import NLog for logging

namespace ElectiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;  // Logger for the UsersController
        private static Logger logger = LogManager.GetLogger("UsersController");  // NLog logger
        private readonly UserContext _userContext;  // Entity Framework DbContext for users

        public UsersController(ILogger<UsersController> logger, UserContext userContext)
        {
            _logger = logger;  // Dependency injection of ILogger
            _userContext = userContext;  // Dependency injection of UserContext
        }

        // Endpoint to register a new user
        [HttpPost("register")]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            if (_userContext == null)
            {
                return NotFound();  // Return a 404 response if UserContext is null
            }
            _userContext.UserList.Add(user);  // Add the user to the UserList DbSet
            logger.Info("User creation");  // Log user creation using NLog
            await _userContext.SaveChangesAsync();  // Save changes to the database
            return user;  // Return the created user
        }

        // Endpoint to log in a user
        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginUser(User user)
        {
            if (_userContext == null)
            {
                logger.Info($"Unsuccessful login: User with username = {user.Username}");
                return NotFound();  // Return a 404 response if UserContext is null
            }
            var userdata = await _userContext.UserList.FindAsync(user.Username);  // Find user in the database
            if (userdata == null)
            {
                logger.Info($"Unsuccessful login: User with username = {user.Username}");
            }
            else
            {
                if (userdata.Password == user.Password)  // Check if password matches
                {
                    logger.Info($"Successful login: User with username = {user.Username}");
                    return userdata;  // Return user data for successful login
                }
            }
            logger.Info($"Unsuccessful login: User with username = {user.Username}");
            return NotFound();  // Return a 404 response for unsuccessful login
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_userContext == null)
            {
                return NotFound();  // Return a 404 response if UserContext is null
            }
            return await _userContext.UserList.ToListAsync();  // Return a list of users from the database
        }

        // GET: api/users/username
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUser(String username)
        {
            if (_userContext == null)
            {
                return NotFound();  // Return a 404 response if UserContext is null
            }
            var user = await _userContext.UserList.FindAsync(username);  // Find user by username
            if (user == null)
            {
                return NotFound();  // Return a 404 response if user is not found
            }
            return user;  // Return the user data
        }

        // PUT: api/users/username
        [HttpPut("{username}")]
        public async Task<ActionResult<User>> UpdateUser(String username, User user)
        {
            if (user.Username != username)
            {
                return BadRequest();  // Return a bad request response if usernames don't match
            }

            _userContext.Entry(user).State = EntityState.Modified;  // Mark the user as modified

            await _userContext.SaveChangesAsync();  // Save changes to the database

            var updatedUser = _userContext.UserList.FirstOrDefaultAsync(x => x.Username == username);  // Find the updated user

            return user;  // Return the updated user
        }

        // DELETE: api/users/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userContext.UserList.FindAsync(id);  // Find user by ID
            if (user == null) return NotFound();  // Return a 404 response if user is not found

            _userContext.UserList.Remove(user);  // Remove user from the UserList DbSet

            await _userContext.SaveChangesAsync();  // Save changes to the database

            return NoContent();  // Return a no content response
        }
    }
}
