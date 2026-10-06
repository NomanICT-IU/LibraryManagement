namespace LibraryManegment.Api.Models;

public class Member
{
    public int Id { get; set; }

    public string MemberId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }


    public int Status { get; set; }
}
