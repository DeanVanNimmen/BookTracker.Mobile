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
            new Book { Id = 1, Title = "EF Core with MySQL", Author = "API Seed", Status = ReadingStatus.Reading },
            new Book { Id = 2, Title = "Database Book", Author = "Server Seed", Status = ReadingStatus.ToRead }
        );
    }
}