
namespace LibraryManegment.Api.Models;

public class IssueBook
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public int UserId { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public Book Book { get; set; } = null!;

    public User User { get; set; } = null!;
}

