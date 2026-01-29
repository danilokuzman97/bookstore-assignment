using BookstoreApplication.Data;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using BookstoreApplication.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BookRepository _bookRepository;
        private AuthorRepository _authorRepository;
        private PublisherRepository _publisherRepository;

        public BooksController(AppDbContext context)
        {
           _bookRepository = new BookRepository(context);
           _authorRepository = new AuthorRepository(context);
           _publisherRepository = new PublisherRepository(context);
        }
        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _bookRepository.GetAllAsync());
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            Book? book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST api/books
        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            Author? author = await _authorRepository.GetByIdAsync(book.AuthorId);
            if (author == null)
            {
                return BadRequest();
            }

            Publisher? publisher = await _publisherRepository.GetByIdAsync(book.PublisherId);
            if (publisher == null)
            {
                return BadRequest();
            }

            book.Author = author;
            book.Publisher = publisher;

            Book newBook = await _bookRepository.AddAsync(book);
            return Ok(book);
        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            Book? existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook == null)
            {
                return BadRequest();
            }

            Author? author = await _authorRepository.GetByIdAsync(book.AuthorId);
            if (author == null)
            {
                return BadRequest();
            }

            Publisher? publisher = await _publisherRepository.GetByIdAsync(book.PublisherId);
            if (publisher == null)
            {
                return BadRequest();
            }

            Book newBook = await _bookRepository.UpdatAsynce(book);
            return Ok(newBook);
        }

        // DELETE api/books/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _bookRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
