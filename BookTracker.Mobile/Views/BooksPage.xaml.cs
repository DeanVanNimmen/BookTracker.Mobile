using BookTracker.Mobile.ViewModels;

namespace BookTracker.Mobile.Views;

public partial class BooksPage : ContentPage
{
    private BooksViewModel ViewModel => (BooksViewModel)BindingContext;

    public BooksPage(BooksViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.LoadBooksCommand.Execute(null);
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Models.Book selected)
        {
            ViewModel.SelectBookCommand.Execute(selected);
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}