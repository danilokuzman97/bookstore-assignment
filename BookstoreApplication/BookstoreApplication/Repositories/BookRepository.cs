using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class BookRepository
    {
        private AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public Book Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public Book Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
            return book;
        }

        public bool Delete(int id)
        {
            Book book = _context.Books.Find(id);
            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }

        public List<Book> GetAll()
        {
            return _context.Books
                .Include(book => book.Publisher)
                .Include(book => book.Author)
                .ToList();
        }

        public Book? GetById(int id)
        {
            return _context.Books.Find(id);
        }

    }
}
