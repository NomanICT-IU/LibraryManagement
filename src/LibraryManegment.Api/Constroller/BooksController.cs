using LibraryManegment.Api.Dtos.BookDtos;
using LibraryManegment.Api.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManegment.Api.Constroller;

[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpGet("get-books")]
    public async Task<IActionResult> GetAll()
    {
        var books = await bookService.GetAllAsync();

        if (books == null || !books.Any())
        {
            return NotFound("No books found.");
        }

        return Ok(books);
    }

    [HttpGet("get-book-by-id/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await bookService.GetByIdAsync(id);

        if (book is null)
        {
            return NotFound($"Book with ID {id} was not found.");
        }

        return Ok(book);
    }

    [HttpPost("create-book")]
    public async Task<IActionResult> Create(CreateBookDto dto)
    {
        var book = await bookService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = book.Id },
            book);
    }

    [HttpPut("update-book/{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateBookDto dto)
    {
        var book = await bookService.UpdateAsync(id, dto);

        if (!book)
        {
            return NotFound($"Book with ID {id} was not found.");
        }

        return Ok(new { Message = "Book updated successfully." });
    }

    [HttpDelete("delete-book/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await bookService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound($"Book with ID {id} was not found.");
        }

        return Ok(new
        {
            Message = "Book deleted successfully."
        });

    }
}