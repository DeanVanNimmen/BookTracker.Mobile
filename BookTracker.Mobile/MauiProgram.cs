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

        // You are running the MAUI app on Windows Machine
        // so localhost is correct:
        var apiBaseAddress = "https://localhost:7033/";

        // Services
        builder.Services.AddSingleton(sp => new HttpClient
        {
            BaseAddress = new Uri(apiBaseAddress)
        });

        builder.Services.AddSingleton<IBookService, ApiBookService>();

        // ViewModels
        builder.Services.AddTransient<BooksViewModel>();
        builder.Services.AddTransient<BookDetailViewModel>();

        // Views
        builder.Services.AddTransient<BooksPage>();
        builder.Services.AddTransient<BookDetailPage>();

        return builder.Build();
    }
}