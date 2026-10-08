namespace LibraryManegment.Api.Dtos.IssueBookDtos;

public class ExtentedRequestDto
{
    public int Id { get; set; }
    public DateTime RequestedDueDate { get; set; }
    public int RoleId { get; set; }
}
