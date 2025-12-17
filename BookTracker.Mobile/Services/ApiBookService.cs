using System.Net.Http.Json;
using BookTracker.Mobile.Models;

namespace BookTracker.Mobile.Services;

public class ApiBookService : IBookService
{
    private readonly HttpClient _httpClient;

    public ApiBookService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Book>> GetBooksAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<List<Book>>("api/books");
        return result ?? new List<Book>();
    }

    public async Task<Book?> GetBookAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Book>($"api/books/{id}");
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        // For create, we ignore book.Id on server side
        book.Id = 0;

        var response = await _httpClient.PostAsJsonAsync("api/books", book);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<Book>();
        return created!;
    }

    public async Task<Book> UpdateBookAsync(Book book)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/books/{book.Id}", book);
        response.EnsureSuccessStatusCode();

        var updated = await response.Content.ReadFromJsonAsync<Book>();
        return updated!;
    }

    public async Task DeleteBookAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/books/{id}");
        response.EnsureSuccessStatusCode();
    }
}