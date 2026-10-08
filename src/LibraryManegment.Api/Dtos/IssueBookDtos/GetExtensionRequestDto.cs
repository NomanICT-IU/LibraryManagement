namespace LibraryManegment.Api.Dtos.IssueBookDtos;

public class GetExtensionRequestDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string BookName { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public DateTime? RequestedDueDate { get; set; }
}
