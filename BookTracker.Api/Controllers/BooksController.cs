using BookTracker.Api.Data;
using BookTracker.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _db;

    public BooksController(AppDbContext db)
    {
        _db = db;
    }

    // GET: api/books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAll()
    {
        var books = await _db.Books
            .OrderBy(b => b.Title)
            .ToListAsync();

        return Ok(books);
    }

    // GET: api/books/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> GetById(int id)
    {
        var book = await _db.Books.FindAsync(id);
        if (book == null) return NotFound();
        return Ok(book);
    }

    // POST: api/books
    [HttpPost]
    public async Task<ActionResult<Book>> Create(Book book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        book.Id = 0; // let database assign
        _db.Books.Add(book);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    // PUT: api/books/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Book>> Update(int id, Book book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existing = await _db.Books.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Title = book.Title;
        existing.Author = book.Author;
        existing.Status = book.Status;
        existing.FinishedOn = book.FinishedOn;

        await _db.SaveChangesAsync();

        return Ok(existing);
    }

    // DELETE: api/books/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _db.Books.FindAsync(id);
        if (existing == null) return NotFound();

        _db.Books.Remove(existing);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}