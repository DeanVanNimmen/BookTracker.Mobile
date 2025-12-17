using System.Windows.Input;
using BookTracker.Mobile.Models;
using BookTracker.Mobile.Services;

namespace BookTracker.Mobile.ViewModels;

[QueryProperty(nameof(BookId), "BookId")]
public class BookDetailViewModel : BaseViewModel
{
    private readonly IBookService _bookService;

    private int _bookId;
    public int BookId
    {
        get => _bookId;
        set
        {
            if (SetProperty(ref _bookId, value))
            {
                LoadBookAsync(value);
                (DeleteCommand as Command)?.ChangeCanExecute();
                (MarkFinishedCommand as Command)?.ChangeCanExecute();
            }
        }
    }

    private string _title = "";
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    private string _author = "";
    public string Author
    {
        get => _author;
        set => SetProperty(ref _author, value);
    }

    private ReadingStatus _status = ReadingStatus.ToRead;
    public ReadingStatus Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }

    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand MarkFinishedCommand { get; }

    public BookDetailViewModel(IBookService bookService)
    {
        _bookService = bookService;
        SaveCommand = new Command(async () => await SaveAsync());
        DeleteCommand = new Command(async () => await DeleteAsync(), () => BookId != 0);
        MarkFinishedCommand = new Command(async () => await MarkFinishedAsync(), () => BookId != 0);
    }

    private async void LoadBookAsync(int id)
    {
        if (id == 0) return;   // new book

        var book = await _bookService.GetBookAsync(id);
        if (book == null) return;

        Title = book.Title;
        Author = book.Author;
        Status = book.Status;
    }

    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Author))
        {
            await Shell.Current.DisplayAlert("Validation", "Title and Author are required", "OK");
            return;
        }

        var book = new Book
        {
            Id = BookId,
            Title = Title,
            Author = Author,
            Status = Status,
            FinishedOn = Status == ReadingStatus.Finished ? DateTime.Today : null
        };

        if (BookId == 0)
            await _bookService.AddBookAsync(book);
        else
            await _bookService.UpdateBookAsync(book);

        await Shell.Current.GoToAsync("..");
    }

    private async Task DeleteAsync()
    {
        if (BookId == 0) return;

        var confirm = await Shell.Current.DisplayAlert("Delete", "Are you sure you want to delete this book?", "Yes", "No");
        if (!confirm) return;

        await _bookService.DeleteBookAsync(BookId);
        await Shell.Current.GoToAsync("..");
    }

    private async Task MarkFinishedAsync()
    {
        Status = ReadingStatus.Finished;
        await SaveAsync();
    }
}