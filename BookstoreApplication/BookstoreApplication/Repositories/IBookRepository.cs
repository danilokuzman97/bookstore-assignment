using BookstoreApplication.Models;


namespace BookstoreApplication.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book> UpdateAsync(Book book);
        Task<Book> AddAsync(Book book);
        Task<bool> DeleteAsync(int id);
    }
}
