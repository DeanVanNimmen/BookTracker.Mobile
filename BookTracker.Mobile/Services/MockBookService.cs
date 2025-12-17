using BookTracker.Mobile.Models;

namespace BookTracker.Mobile.Services;

public interface IBookService
{
    Task<List<Book>> GetBooksAsync();
    Task<Book?> GetBookAsync(int id);
    Task<Book> AddBookAsync(Book book);
    Task<Book> UpdateBookAsync(Book book);
    Task DeleteBookAsync(int id);
}

public class MockBookService : IBookService
{
    private readonly List<Book> _books = new()
    {
        new Book { Id = 1, Title = "The Pragmatic Programmer", Author = "Hunt & Thomas", Status = ReadingStatus.Reading },
        new Book { Id = 2, Title = "Clean Code", Author = "Robert C. Martin", Status = ReadingStatus.ToRead },
        new Book { Id = 3, Title = "Atomic Habits", Author = "James Clear", Status = ReadingStatus.Finished, FinishedOn = DateTime.Today.AddDays(-10) }
    };

    private int _nextId = 4;

    public Task<List<Book>> GetBooksAsync()
        => Task.FromResult(_books.OrderBy(b => b.Title).ToList());

    public Task<Book?> GetBookAsync(int id)
        => Task.FromResult(_books.FirstOrDefault(b => b.Id == id));

    public Task<Book> AddBookAsync(Book book)
    {
        book.Id = _nextId++;
        _books.Add(book);
        return Task.FromResult(book);
    }

    public Task<Book> UpdateBookAsync(Book book)
    {
        var existing = _books.FirstOrDefault(b => b.Id == book.Id);
        if (existing is null)
            throw new InvalidOperationException("Book not found");

        existing.Title = book.Title;
        existing.Author = book.Author;
        existing.Status = book.Status;
        existing.FinishedOn = book.FinishedOn;

        return Task.FromResult(existing);
    }

    public Task DeleteBookAsync(int id)
    {
        var existing = _books.FirstOrDefault(b => b.Id == id);
        if (existing != null)
            _books.Remove(existing);

        return Task.CompletedTask;
    }
}