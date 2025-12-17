using BookTracker.Api.Models;
using BookTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _repository;

    public BooksController(IBookRepository repository)
    {
        _repository = repository;
    }

    // GET: api/books
    [HttpGet]
    public ActionResult<IEnumerable<BookDto>> GetAll()
    {
        var books = _repository.GetAll();
        return Ok(books);
    }

    // GET: api/books/5
    [HttpGet("{id:int}")]
    public ActionResult<BookDto> GetById(int id)
    {
        var book = _repository.GetById(id);
        if (book == null) return NotFound();
        return Ok(book);
    }

    // POST: api/books
    [HttpPost]
    public ActionResult<BookDto> Create(BookDto book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // ignore incoming Id, set in repository
        book.Id = 0;
        var created = _repository.Add(book);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/books/5
    [HttpPut("{id:int}")]
    public ActionResult<BookDto> Update(int id, BookDto book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = _repository.Update(id, book);
        if (updated == null) return NotFound();

        return Ok(updated);
    }

    // DELETE: api/books/5
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var success = _repository.Delete(id);
        if (!success) return NotFound();

        return NoContent();
    }
}