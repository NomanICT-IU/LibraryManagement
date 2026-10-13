using LibraryManegment.Api.Dtos.BookDtos;
using LibraryManegment.Api.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManegment.Api.Constroller;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
[AllowAnonymous]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpGet("get-books")]
    //[Authorize(Policy = "Book.Read")]
    public async Task<IActionResult> GetAll()
    {
        var books = await bookService.GetAllAsync();

        if (books == null || !books.Any())
        {
            return NotFound("No books found.");
        }

        return Ok(books);
    }

    [HttpGet("get-book-by-title")]
    //[Authorize(Policy = "Book.Read")]
    public async Task<IActionResult> GetByTitle(string title)
    {
        var book = await bookService.GetByTitleAsync(title);

        if (book is null)
        {
            return NotFound($"Book with title {title} was not found.");
        }

        return Ok(book);
    }

    [HttpPost("create-book")]
    //[Authorize(Policy = "Book.Create")]
    public async Task<IActionResult> Create(CreateBookDto dto)
    {
        var book = await bookService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetByTitle),
            new { title = book.Name },
            book);
    }

    [HttpPut("update-book/{id:int}")]
    //[Authorize(Policy = "Book.Update")]
    public async Task<IActionResult> Update(int id, UpdateBookDto dto)
    {
        var updated = await bookService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound($"Book with ID {id} was not found.");
        }

        return Ok(new { Message = "Book updated successfully." });
    }


    //[HttpDelete("delete-book/{id:int}")]
    ////[Authorize(Policy = "Book.Delete")]
    //public async Task<IActionResult> Delete(int id)
    //{
    //    var deleted = await bookService.DeleteAsync(id);

    //    if (!deleted)
    //    {
    //        return NotFound($"Book with ID {id} was not found.");
    //    }

    //    return Ok(new { Message = "Book deleted successfully." });
    //}

    [HttpPatch("add-quantity")]
    public async Task<IActionResult> AddQuantity([FromBody] AddQuantityDto dto)
    {
        if (dto.Quantity <= 0)
            return BadRequest("Quantity must be greater than zero.");

        var result = await bookService.AddQuantityAsync(dto);

        if (!result)
            return NotFound("Book not found.");

        return Ok(
        "Book quantity added successfully."
        );
    }
}