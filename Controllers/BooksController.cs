// Controllers/BooksController.cs
using GeneradorCompras.Models.Interface;
using GeneradorCompras.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("fake/{count}")]
    public ActionResult<List<Book>> GetFakeBooks(int count)
    {
        var books = _bookService.GetFakeBooks(count);
        return Ok(books);
    }
}