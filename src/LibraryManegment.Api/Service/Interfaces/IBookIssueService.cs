

using LibraryManegment.Api.Dtos.BookIssue;

namespace LibraryManegment.Api.Service.Interfaces;

public interface IBookIssueService
{
    Task<bool> IssueBookAsync(CreateBookIssueDto bookIssueDto);
    Task<bool> ReturnBookIssueAsync(ReturnBookIssueDto bookIssueDto);
}
