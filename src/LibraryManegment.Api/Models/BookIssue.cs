
namespace LibraryManegment.Api.Models;

public class BookIssue
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public int UserInfoId { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public Book Book { get; set; } = null!;

    public UserInfo UserInfo { get; set; } = null!;
}

