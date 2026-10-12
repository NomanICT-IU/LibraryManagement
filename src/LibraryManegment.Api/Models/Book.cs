namespace LibraryManegment.Api.Models;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Quantity { get; set; }

    public ICollection<IssueBook> IssueBooks { get; set; }

}
