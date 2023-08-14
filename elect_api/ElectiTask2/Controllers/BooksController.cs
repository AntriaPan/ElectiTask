using ElectiTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;


namespace ElectiTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private static Logger logger = LogManager.GetLogger("BooksController");
        private readonly BookContext _bookContext;

        public BooksController(ILogger<BooksController> logger, BookContext bookContext)
        {
            _logger = logger; 
            _bookContext = bookContext;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            if (_bookContext == null)
            {
                return NotFound();
            }

            _bookContext.BookList.Add(book);
            logger.Info("Book creation"); // Use NLog.Logger methods
            await _bookContext.SaveChangesAsync();
            return book;
        }
        //Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        {
            if (_bookContext == null)
            {
                return NotFound();
            }
            return await _bookContext.BookList.ToListAsync();
        }

        //GET : api/books/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(Guid id)
        {
            if (_bookContext == null)
            {
                return NotFound();
            }
            var book = await _bookContext.BookList.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(Guid id, Book updatedBook)
        {
            if (id != updatedBook.Id)
            {
                return BadRequest();
            }

            _bookContext.Entry(updatedBook).State = EntityState.Modified;
            await _bookContext.SaveChangesAsync();

            logger.Info($"Book with ID {id} updated");

            return Ok(updatedBook); // Return Ok result with the updated book
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var book = await _bookContext.BookList.FindAsync(id);
            if (book == null) return NotFound();

            _bookContext.BookList.Remove(book);
            logger.Info($"Book with ID {id} deleted");

            await _bookContext.SaveChangesAsync();

            return NoContent();

        }
    }
}


