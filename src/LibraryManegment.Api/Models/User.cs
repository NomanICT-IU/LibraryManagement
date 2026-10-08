namespace LibraryManegment.Api.Models;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public int RoleId { get; set; }
    // Navigation Property
    public Role Role { get; set; } = null!;
    public int Status { get; set; }
}
