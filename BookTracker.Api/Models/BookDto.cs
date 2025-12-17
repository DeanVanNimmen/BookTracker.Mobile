namespace BookTracker.Api.Models;

public enum ReadingStatus
{
    ToRead,
    Reading,
    Finished
}

public class BookDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public ReadingStatus Status { get; set; } = ReadingStatus.ToRead;
    public DateTime? FinishedOn { get; set; }
}