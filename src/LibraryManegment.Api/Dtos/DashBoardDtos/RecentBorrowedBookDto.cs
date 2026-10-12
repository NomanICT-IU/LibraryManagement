namespace LibraryManegment.Api.Dtos.DashBoardDtos;

public class RecentBorrowedBookDto
{
    public string BookTitle { get; set; }
    public string Member { get; set; }
    public string IssuedDate { get; set; }
    public string DueDate { get; set; }
}
