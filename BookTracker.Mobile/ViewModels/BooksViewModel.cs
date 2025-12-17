using System.Collections.ObjectModel;
using System.Windows.Input;
using BookTracker.Mobile.Models;
using BookTracker.Mobile.Services;

namespace BookTracker.Mobile.ViewModels;

public class BooksViewModel : BaseViewModel
{
    private readonly IBookService _bookService;

    public ObservableCollection<Book> Books { get; } = new();

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public ICommand LoadBooksCommand { get; }
    public ICommand AddBookCommand { get; }
    public ICommand SelectBookCommand { get; }

    public BooksViewModel(IBookService bookService)
    {
        _bookService = bookService;
        LoadBooksCommand = new Command(async () => await LoadBooksAsync());
        AddBookCommand = new Command(async () => await GoToAddBookAsync());
        SelectBookCommand = new Command<Book>(async (book) => await GoToDetailAsync(book));
    }

    private async Task LoadBooksAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            Books.Clear();
            var items = await _bookService.GetBooksAsync();
            foreach (var book in items)
                Books.Add(book);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private Task GoToAddBookAsync()
        => Shell.Current.GoToAsync(nameof(Views.BookDetailPage));

    private Task GoToDetailAsync(Book? book)
    {
        if (book == null) return Task.CompletedTask;

        var route = $"{nameof(Views.BookDetailPage)}?BookId={book.Id}";
        return Shell.Current.GoToAsync(route);
    }
}