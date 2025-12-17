namespace BookTracker.Api.Entities;

public enum ReadingStatus
{
    ToRead,
    Reading,
    Finished
}

public class Book
{
    public int Id { get; set; }                     // PK
    public required string Title { get; set; }
    public required string Author { get; set; }
    public ReadingStatus Status { get; set; } = ReadingStatus.ToRead;
    public DateTime? FinishedOn { get; set; }
}