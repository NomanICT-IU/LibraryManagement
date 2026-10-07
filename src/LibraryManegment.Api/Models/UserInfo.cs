namespace LibraryManegment.Api.Models;

public class UserInfo
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public int Status { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;

    public Role Role { get; set; } = null!;
}
