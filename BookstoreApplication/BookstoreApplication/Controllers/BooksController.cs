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
        public IActionResult GetAll()
        {
            return Ok(_bookRepository.GetAll());
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Book? book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST api/books
        [HttpPost]
        public IActionResult Post(Book book)
        {
            // kreiranje knjige je moguće ako je izabran postojeći autor
            Author? author = _authorRepository.GetById(book.AuthorId);
            if (author == null)
            {
                return BadRequest();
            }

            // kreiranje knjige je moguće ako je izabran postojeći izdavač
            Publisher? publisher = _publisherRepository.GetById(book.PublisherId);
            if (publisher == null)
            {
                return BadRequest();
            }

            book.Author = author;
            book.Publisher = publisher;

            Book newBook = _bookRepository.Add(book);
            return Ok(book);
        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            // izmena knjige je moguca ako je izabran postojeći autor
            Author? author = _authorRepository.GetById(book.AuthorId);
            if (author == null)
            {
                return BadRequest();
            }

            // izmena knjige je moguca ako je izabran postojeći izdavač
            Publisher? publisher = _publisherRepository.GetById(book.PublisherId);
            if (publisher == null)
            {
                return BadRequest();
            }

            Book newBook = _bookRepository.Update(book);
            return Ok(newBook);
        }

        // DELETE api/books/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool deleted = _bookRepository.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
