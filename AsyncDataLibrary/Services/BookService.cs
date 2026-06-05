using AsyncDataLibrary.Interfaces;
using AsyncDataLibrary.Models;

namespace AsyncDataLibrary.Services;

public class BookService : IService<Book>
{
    private readonly IRepository<Book> _repository;

    public BookService(IRepository<Book> repository)
    {
        _repository = repository;
    }

    public void Add(Book item)
    {
        PrepareBook(item);
        _repository.Add(item);
    }

    public async Task AddAsync(Book item)
    {
        PrepareBook(item);
        await _repository.AddAsync(item);
    }

    public List<Book> GetAll()
    {
        return _repository.GetAll();
    }

    public Task<List<Book>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Book? GetById(Guid id)
    {
        return _repository.GetById(id);
    }

    public Task<Book?> GetByIdAsync(Guid id)
    {
        return _repository.GetByIdAsync(id);
    }

    public List<Book> GetAvailableBooks()
    {
        return _repository.GetAll().Where(book => book.IsAvailable).ToList();
    }

    public async Task<List<Book>> GetAvailableBooksAsync()
    {
        List<Book> books = await _repository.GetAllAsync();
        return books.Where(book => book.IsAvailable).ToList();
    }

    public void Update(Book item)
    {
        _repository.Update(item);
    }

    public Task UpdateAsync(Book item)
    {
        return _repository.UpdateAsync(item);
    }

    public void Delete(Guid id)
    {
        _repository.Delete(id);
    }

    public Task DeleteAsync(Guid id)
    {
        return _repository.DeleteAsync(id);
    }

    private static void PrepareBook(Book book)
    {
        if (book.Id == Guid.Empty)
        {
            book.Id = Guid.NewGuid();
        }
    }
}
