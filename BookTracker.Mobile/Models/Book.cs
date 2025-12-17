namespace BookTracker.Mobile.Models;

public enum ReadingStatus
{
    ToRead,
    Reading,
    Finished
}

public class Book
{
    public int Id { get; set; }              // Local ID for now
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public ReadingStatus Status { get; set; } = ReadingStatus.ToRead;
    public DateTime? FinishedOn { get; set; }
}