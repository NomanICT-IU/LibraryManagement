namespace LibraryManegment.Api.Dtos.BookDtos;

public class BookDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Author { get; set; }

    public string ISBN { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int Quantity { get; set; }

}
