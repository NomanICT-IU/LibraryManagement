namespace LibraryManegment.Api.Dtos.BookDtos;

public class UpdateBookDto
{
    public string Name { get; set; }

    public string Author { get; set; }

    public string ISBN { get; set; } = null!;

    public int Quantity { get; set; }

}
