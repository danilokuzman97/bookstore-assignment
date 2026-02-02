using BookstoreApplication.Models;
using BookstoreApplication.DTOs;

namespace BookstoreApplication.Services
{
    public interface IBookService
    {
        Task Add(Book book);
        Task<bool> Delete(int id);
        Task<List<BookDTO>> GetAll();
        Task<BookDetailsDto?> GetOne(int id);
        Task Update(Book book);
    }
}