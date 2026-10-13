namespace LibraryManegment.Api.Dtos.DateExtensionDtos;


public class DueDateExtensionListResponseDto
{
    public int TotalCount { get; set; }
    public IEnumerable<DueDateExtensionDetailListDto> Requests { get; set; }
}
public class DueDateExtensionDetailListDto
{
    public string Name { get; set; }
    public string Title { get; set; }
    public DateTime CurrentDueDate { get; set; }
    public DateTime? ExtenedDueDate { get; set; }
    public int? Status { get; set; }
}