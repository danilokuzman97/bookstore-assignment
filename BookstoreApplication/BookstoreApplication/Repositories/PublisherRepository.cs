using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class PublisherRepository
    {
        private AppDbContext _context;

        public PublisherRepository(AppDbContext context)
        {
            _context = context;
        }

        public Publisher Add(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
            return publisher;
        }

        public Publisher Update(Publisher publisher)
        {
            Publisher existing = _context.Publishers.Find(publisher.Id);

            existing.Name = publisher.Name;
            existing.Address = publisher.Address;
            existing.Website = publisher.Website;

            _context.SaveChanges();
            return existing;
        }


        public bool Delete(int id)
        {
            Publisher publisher = _context.Publishers.Find(id);
            if (publisher == null)
            {
                return false;
            }

            _context.Publishers.Remove(publisher);
            _context.SaveChanges();
            return true;
        }

        public List<Publisher> GetAll()
        {
            return _context.Publishers.ToList();
        }

        public Publisher? GetById(int id)
        {
            return _context.Publishers.Find(id);
        }

    }
}
