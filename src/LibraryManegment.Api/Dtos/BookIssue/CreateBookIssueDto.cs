namespace LibraryManegment.Api.Dtos.BookIssue;

public class CreateBookIssueDto
{
    public int BookId { get; set; }
    public int UserInfoId { get; set; }
    public DateTime DueDate { get; set; }
}
