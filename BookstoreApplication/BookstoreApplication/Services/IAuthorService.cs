using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Services
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAll();
        Task<Author?> GetOne(int id);
        Task Update(Author author);
        Task Add(Author author);
        Task<bool> Delete(int id);
        Task<PaginatedList<AuthorDTO>> GetAllPaged(int page);
    }
}
