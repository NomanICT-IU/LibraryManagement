namespace LibraryManegment.Api.Dtos.DashBoardDtos;

public class DashboardOverviewDto
{
    public int TotalBook { get; set; }
    public int AvailableCopies { get; set; }
    public int BorrowedCopies { get; set; }
    public int AllMembers { get; set; }
    public IEnumerable<RecentBorrowedBookDto> RecentBorrowedBooks { get; set; }
}
