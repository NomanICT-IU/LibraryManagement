

using LibraryManegment.Api.Dtos.BookIssue;

namespace LibraryManegment.Api.Service.Interfaces;

public interface IBookIssueService
{
    Task<bool> IssueBookAsync(CreateIssueBookDto bookIssueDto);
    Task<bool> ReturnBookIssueAsync(ReturnIssueBookDto bookIssueDto);
}
