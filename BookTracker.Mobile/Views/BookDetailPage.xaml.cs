using BookTracker.Mobile.Models;
using BookTracker.Mobile.ViewModels;

namespace BookTracker.Mobile.Views;

public partial class BookDetailPage : ContentPage
{
    private BookDetailViewModel ViewModel => (BookDetailViewModel)BindingContext;

    public BookDetailPage(BookDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        // keep Picker and Status in sync (very simple approach)
        StatusPicker.SelectedIndexChanged += (s, e) =>
        {
            if (StatusPicker.SelectedIndex == 0) ViewModel.Status = ReadingStatus.ToRead;
            else if (StatusPicker.SelectedIndex == 1) ViewModel.Status = ReadingStatus.Reading;
            else if (StatusPicker.SelectedIndex == 2) ViewModel.Status = ReadingStatus.Finished;
        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Set Picker based on current Status
        StatusPicker.SelectedIndex = ViewModel.Status switch
        {
            ReadingStatus.ToRead => 0,
            ReadingStatus.Reading => 1,
            ReadingStatus.Finished => 2,
            _ => 0
        };
    }
}