

using LibraryManegment.Api.Dtos.BookIssue;
using LibraryManegment.Api.Dtos.IssueBookDtos;

namespace LibraryManegment.Api.Service.Interfaces;

public interface IIssueBookService
{
    Task<bool> IssueBookAsync(CreateIssueBookDto issueBookDto);
    Task<bool> ReturnIssueBookAsync(ReturnIssueBookDto issueBookDto);
    Task<List<IssueBookDto>> GetIssueBooksAsync();
    Task<IssueBookDto> GetIssueBookByIdAsync(int id);
    Task<List<IssueBookDto>> GetIssueBooksByEmailAsync(string email);
    Task<bool> RequestDueDateExtensionAsync(ExtentedRequestDto requestDto);
    Task<List<GetExtensionRequestDto>> GetExtensionRequestsAsync(int roleId);
    Task<bool> ApproveExtensionRequestAsync(int id);
    Task<bool> RejectExtensionRequestAsync(int id);
}
