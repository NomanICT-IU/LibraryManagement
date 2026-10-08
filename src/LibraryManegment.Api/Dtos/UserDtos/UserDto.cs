namespace LibraryManegment.Api.Dtos.UserDtos;

public class UserDto
{
    public int Id { get; set; }
    //public int RoleId { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int TotalBooksBorrowed { get; set; } = 0;
}
