namespace LibraryManegment.Api.Dtos.BookIssue;

public class CreateIssueBookDto
{
    public int BookId { get; set; }
    public int UserInfoId { get; set; }
    public DateTime DueDate { get; set; }
}
