namespace LibraryManegment.Api.Dtos.BookDtos;

public class CreateBookDto
{
    public string Name { get; set; }

    public string Author { get; set; }

    public string ISBN { get; set; } = null!;

    public int Quantity { get; set; }
}
