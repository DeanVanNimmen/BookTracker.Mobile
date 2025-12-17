using BookTracker.Mobile.Services;
using BookTracker.Mobile.ViewModels;
using BookTracker.Mobile.Views;

namespace BookTracker.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Services
        builder.Services.AddSingleton<IBookService, MockBookService>();

        // ViewModels
        builder.Services.AddTransient<BooksViewModel>();
        builder.Services.AddTransient<BookDetailViewModel>();

        // Views
        builder.Services.AddTransient<BooksPage>();
        builder.Services.AddTransient<BookDetailPage>();

        return builder.Build();
    }
}