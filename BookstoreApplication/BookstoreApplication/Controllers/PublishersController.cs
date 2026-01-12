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
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        // GET api/publishers/id
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Publisher? publisher = _repository.GetById(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        // POST api/publishers
        [HttpPost]
        public IActionResult Post(Publisher publisher)
        {
            Publisher newPublisher = _repository.Add(publisher);
            return Ok(newPublisher);
        }

        // PUT api/publishers/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }

            Publisher? existingPublisher = _repository.GetById(id);
            if (existingPublisher == null)
            {
                return NotFound();
            }

            Publisher newPublisher = _repository.Update(publisher);
            return Ok(newPublisher);
        }

        // DELETE api/publishers/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool deleted = _repository.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
