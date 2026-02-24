using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Serilog;
using ILogger = Serilog.ILogger;

namespace BookstoreApplication.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = Log.ForContext<BookService>();
        }

        public async Task<List<BookDTO>> GetAll()
        {
            _logger.Information("Get all books");

            var books = await _bookRepository.GetAllAsync();
            return books.Select(_mapper.Map<BookDTO>).ToList();
        }

        public async Task<BookDetailsDto?> GetOne(int id)
        {
            _logger.Information($"Get a book with {id}", id);

            var book = await _bookRepository.GetByIdAsync(id);
            if(book == null)
            {
                _logger.Warning($"Book with {id} not found", id);
                throw new NotFoundException(id);
            }
            return _mapper.Map<BookDetailsDto?>(book);

        }

        public async Task Update(Book book)
        {
            _logger.Information("Update book", book.Id);

            var theBook = await _bookRepository.GetByIdAsync(book.Id);
            if(theBook == null)
            {
                _logger.Warning("book not found for update", book.Id);
                throw new NotFoundException(book.Id);
            }
            await _bookRepository.UpdateAsync(book);
            _logger.Information("Book updated successfully", book.Id);
        }

        public async Task Add(Book book)
        {
            _logger.Information("Add new book", book.Title);

            if (book.AuthorId <= 0)
            {
                _logger.Warning("Invalid Author while adding book");
                throw new BadRequestException("AuthorId is invalid.");
            }
            if (book.PublisherId <= 0)
            {
                _logger.Warning("Invalid PublisherId while adding book");
                throw new BadRequestException("Publisher is invalid.");
            }

            await _bookRepository.AddAsync(book);
            _logger.Information("Book added successfully.");
        }

        public async Task<bool> Delete(int id)
        {
            _logger.Information("Delete book", id);
            var deleted = await _bookRepository.DeleteAsync(id);
            if (!deleted)
            {
                _logger.Warning("Book not found, cant delete.", id);
                throw new NotFoundException(id);
            }
            _logger.Information("Book deleted sucessfuly.", id);
            return true;

        }
    }
}
