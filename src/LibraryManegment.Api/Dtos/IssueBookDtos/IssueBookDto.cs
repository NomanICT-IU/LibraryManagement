namespace LibraryManegment.Api.Dtos.IssueBookDtos;

public class IssueBookDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string BookName { get; set; } = null!;
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
}
