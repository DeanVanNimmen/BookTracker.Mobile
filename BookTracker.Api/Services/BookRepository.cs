using BookTracker.Api.Models;

namespace BookTracker.Api.Services;

public interface IBookRepository
{
    IEnumerable<BookDto> GetAll();
    BookDto? GetById(int id);
    BookDto Add(BookDto book);
    BookDto? Update(int id, BookDto book);
    bool Delete(int id);
}

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<BookDto> _books = new()
    {
        new BookDto { Id = 1, Title = "The Pragmatic Programmer", Author = "Hunt & Deano", Status = ReadingStatus.Reading },
        new BookDto { Id = 2, Title = "Clean Code", Author = "Robert C. Martin", Status = ReadingStatus.ToRead },
        new BookDto { Id = 3, Title = "Atomic Habits", Author = "James Clear", Status = ReadingStatus.Finished, FinishedOn = DateTime.UtcNow.AddDays(-10) }
    };

    private int _nextId = 4;

    public IEnumerable<BookDto> GetAll() => _books.OrderBy(b => b.Title);

    public BookDto? GetById(int id) => _books.FirstOrDefault(b => b.Id == id);

    public BookDto Add(BookDto book)
    {
        book.Id = _nextId++;
        _books.Add(book);
        return book;
    }

    public BookDto? Update(int id, BookDto book)
    {
        var existing = GetById(id);
        if (existing == null) return null;

        existing.Title = book.Title;
        existing.Author = book.Author;
        existing.Status = book.Status;
        existing.FinishedOn = book.FinishedOn;

        return existing;
    }

    public bool Delete(int id)
    {
        var existing = GetById(id);
        if (existing == null) return false;

        _books.Remove(existing);
        return true;
    }
}