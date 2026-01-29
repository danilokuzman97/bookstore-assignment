using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorRepository _repository;

        public AuthorsController(AppDbContext context)
        {
            _repository = new AuthorRepository(context);
        }

        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            Author? author = await _repository.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        // POST api/authors
        [HttpPost]
        public async Task<IActionResult> Post(Author author)
        {
            author.Id = 0;
            Author newAuthor = await _repository.AddAsync(author);
            return Ok(newAuthor);
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            Author? existingAuthor = await _repository.GetByIdAsync(id);
            if (existingAuthor == null)
            {
                return NotFound();
            }

            Author updatedAuthor = await _repository.UpdateAsync(author);
            return Ok(updatedAuthor);

        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
