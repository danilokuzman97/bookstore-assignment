using BookstoreApplication.Models;

namespace BookstoreApplication.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author> UpdateAsync(Author author);
        Task<Author> AddAsync(Author author);
        Task<bool> DeleteAsync(int id);
    }
}
