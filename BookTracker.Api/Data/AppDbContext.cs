using BookTracker.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookTracker.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed some initial data (optional, for testing)
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Book One", Author = "John AppleSeed", Status = ReadingStatus.Reading },
            new Book { Id = 2, Title = "Book Two", Author = "Server Seed", Status = ReadingStatus.ToRead }
        );
    }
}