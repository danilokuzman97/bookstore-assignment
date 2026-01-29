using BookstoreApplication.Data;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {

        private readonly PublisherService _publisherService;

        public PublishersController(AppDbContext context)
        {
            _publisherService = new PublisherService(context);
        }

        // GET: api/publishers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _publisherService.GetAll());
        }

        // GET api/publishers/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            Publisher? publisher = await _publisherService.GetOne(id);
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
            await _publisherService.Add(publisher);
            return Ok(publisher);
        }

        // PUT api/publishers/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }


            await _publisherService.Update(publisher);
            return Ok(publisher);
        }

        // DELETE api/publishers/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _publisherService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
