namespace LibraryManegment.Api.Dtos.DateExtensionDtos;

public class DueDateExtensionResponseDto
{
    public int TotalCount { get; set; }
    public IEnumerable<DueDateExtensionDetailDto> Requests { get; set; }
}
public class DueDateExtensionDetailDto
{
    public string Name { get; set; }
    public string Title { get; set; }
    public DateTime CurrentDueDate { get; set; }
    public DateTime? ExtenedDueDate { get; set; }
}