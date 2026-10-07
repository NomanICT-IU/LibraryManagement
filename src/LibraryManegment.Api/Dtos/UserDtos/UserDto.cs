namespace LibraryManegment.Api.Dtos.UserDtos;

public class UserDto
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;
    public int TotalBooksBorrowed { get; set; } = 0;
}
