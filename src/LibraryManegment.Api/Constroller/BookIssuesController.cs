
using LibraryManegment.Api.Dtos.BookIssue;
using LibraryManegment.Api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManegment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookIssuesController(
    IBookIssueService bookIssueService) : ControllerBase
{
    [HttpPost("issue-book")]
    public async Task<IActionResult> IssueBook(
        [FromBody] CreateBookIssueDto bookIssueDto)
    {
        var result = await bookIssueService
            .IssueBookAsync(bookIssueDto);

        if (!result)
        {
            return BadRequest(
                "Book is not available or User does not exist.");
        }

        return Ok(new
        {
            message = "Book issued successfully."
        });
    }

    [HttpPut("return-book")]
    public async Task<IActionResult> ReturnBook(
        [FromBody] ReturnBookIssueDto bookIssueDto)
    {
        var result = await bookIssueService
            .ReturnBookIssueAsync(bookIssueDto);

        if (!result)
        {
            return BadRequest(
                "Book issue does not exist or the book has already been returned.");
        }

        return Ok(new
        {
            message = "Book returned successfully."
        });
    }
}

