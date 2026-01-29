using BookstoreApplication.Data;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using BookstoreApplication.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {

        private PublisherRepository _repository;

        public PublishersController(AppDbContext context)
        {
            _repository = new PublisherRepository(context);
        }

        // GET: api/publishers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        // GET api/publishers/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            Publisher? publisher = await _repository.GetByIdAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        // POST api/publishers
        [HttpPost]
        public async Task<IActionResult> Post(Publisher publisher)
        {
            publisher.Id = 0;
            Publisher newPublisher = await _repository.AddAsync(publisher);
            return Ok(newPublisher);
        }

        // PUT api/publishers/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }

            Publisher? existingPublisher = await _repository.GetByIdAsync(id);
            if (existingPublisher == null)
            {
                return NotFound();
            }

            Publisher newPublisher = await _repository.UpdateAsync(publisher);
            return Ok(newPublisher);
        }

        // DELETE api/publishers/id
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
