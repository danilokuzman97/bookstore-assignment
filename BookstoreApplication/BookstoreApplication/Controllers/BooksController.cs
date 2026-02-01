using BookstoreApplication.Data;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;


namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IPublisherService _publisherService;

        public BooksController(IBookService bookService, IAuthorService authorService, IPublisherService publisherService)
        {
           _bookService = bookService;
           _authorService = authorService;
           _publisherService = publisherService;
        }
        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _bookService.GetAll());
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            Book? book = await _bookService.GetOne(id);
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
            Author? author = await _authorService.GetOne(book.AuthorId);
            if (author == null)
            {
                return BadRequest();
            }

            Publisher? publisher = await _publisherService.GetOne(book.PublisherId);
            if (publisher == null)
            {
                return BadRequest();
            }

            book.Author = author;
            book.Publisher = publisher;

            await _bookService.Add(book);
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

            Author? author = await _authorService.GetOne(book.AuthorId);
            if (author == null)
            {
                return BadRequest();
            }

            Publisher? publisher = await _publisherService.GetOne(book.PublisherId);
            if (publisher == null)
            {
                return BadRequest();
            }

            await _bookService.Update(book);
            return Ok(book);
        }

        // DELETE api/books/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _bookService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
