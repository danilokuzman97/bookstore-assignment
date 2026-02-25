using BookstoreApplication.Data;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using BookstoreApplication.DTOs;
using Microsoft.AspNetCore.Authorization;


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
            var books = await _bookService.GetAll();

            return Ok(books);
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var book = await _bookService.GetOne(id);

            return Ok(book);
        }

        // POST api/books
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            await _bookService.Add(book);
            return Ok(book);
        }

        // PUT api/books/5
        [Authorize(Roles = "Editor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Book book)
        {
            await _bookService.Update(book);
            return Ok(book);
        }

        // DELETE api/books/id
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.Delete(id);
            return NoContent();
        }
    }
}
