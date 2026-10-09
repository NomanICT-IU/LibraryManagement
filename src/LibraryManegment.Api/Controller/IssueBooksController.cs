using LibraryManegment.Api.Dtos.BookIssue;
using LibraryManegment.Api.Dtos.IssueBookDtos;
using LibraryManegment.Api.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManegment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class IssueBooksController(IIssueBookService bookIssueService) : ControllerBase
{
    [HttpGet("get-issue-books")]
    [Authorize(Policy = "BookIssue.Read")]
    public async Task<IActionResult> GetIssueBooks()
    {
        var result = await bookIssueService.GetIssueBooksAsync();

        if (result is null || result.Count == 0)
            return NotFound("No issued books found.");

        return Ok(result);
    }

    [HttpGet("issue-book-by-id/{id:int}")]
    [Authorize(Policy = "BookIssue.Read")]
    public async Task<IActionResult> GetIssueBookById(int id)
    {
        var result = await bookIssueService.GetIssueBookByIdAsync(id);

        if (result is null)
            return NotFound("Issued book not found.");

        return Ok(result);
    }

    [HttpGet("issue-books-by-email")]
    [Authorize(Policy = "BookIssue.Read")]
    public async Task<IActionResult> GetIssueBooksByEmail([FromQuery] string email)
    {
        var result = await bookIssueService.GetIssueBooksByEmailAsync(email);

        if (result.Count == 0)
            return NotFound("No issued books found for this email.");

        return Ok(result);
    }

    [HttpPost("issue-book")]
    [Authorize(Policy = "BookIssue.Create")]
    public async Task<IActionResult> IssueBook(
        [FromBody] CreateIssueBookDto bookIssueDto)
    {
        var result = await bookIssueService.IssueBookAsync(bookIssueDto);

        if (!result)
            return BadRequest("Book is not available or user does not exist.");

        return Ok(new
        {
            message = "Book issued successfully."
        });
    }

    [HttpPut("return-book")]
    [Authorize(Policy = "BookIssue.Return")]
    public async Task<IActionResult> ReturnBook(
        [FromBody] ReturnIssueBookDto bookIssueDto)
    {
        var result = await bookIssueService.ReturnIssueBookAsync(bookIssueDto);

        if (!result)
            return BadRequest(
                "Book issue does not exist or the book has already been returned.");

        return Ok(new
        {
            message = "Book returned successfully."
        });
    }

    [HttpPut("request-extension")]
    [Authorize(Policy = "ExtensionRequest.Create")]
    public async Task<IActionResult> RequestExtension(
        [FromBody] ExtentedRequestDto requestDto)
    {
        var result = await bookIssueService.RequestDueDateExtensionAsync(requestDto);

        if (!result)
            return NotFound(
                "Issue book not found or the book has already been returned.");

        return Ok("Due date extension request submitted successfully.");
    }

    [HttpGet("get-all-extension-requests")]
    [Authorize(Policy = "ExtensionRequest.Read")]
    public async Task<IActionResult> GetExtensionRequests([FromQuery] int roleId)
    {
        var result = await bookIssueService.GetExtensionRequestsAsync(roleId);

        if (result.Count == 0)
            return NotFound("No extension requests found.");

        return Ok(result);
    }

    [HttpPatch("approve-extension/{id:int}")]
    [Authorize(Policy = "ExtensionRequest.Approve")]
    public async Task<IActionResult> ApproveExtension(int id)
    {
        var result = await bookIssueService.ApproveExtensionRequestAsync(id);

        if (!result)
            return BadRequest("Extension request cannot be approved.");

        return Ok("Extension request approved successfully.");
    }

    [HttpPatch("reject-extension/{id:int}")]
    [Authorize(Policy = "ExtensionRequest.Reject")]
    public async Task<IActionResult> RejectExtension(int id)
    {
        var result = await bookIssueService.RejectExtensionRequestAsync(id);

        if (!result)
            return BadRequest("Extension request cannot be rejected.");

        return Ok("Extension request rejected successfully.");
    }
}