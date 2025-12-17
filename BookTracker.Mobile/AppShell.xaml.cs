namespace BookTracker.Mobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(Views.BookDetailPage), typeof(Views.BookDetailPage));
    }
}