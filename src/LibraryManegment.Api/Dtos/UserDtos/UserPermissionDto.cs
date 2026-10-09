namespace LibraryManegment.Api.Dtos.UserDtos;

public class UserPermissionDto
{
    public int UserId { get; set; }

    public string RoleName { get; set; } = null!;

    public string PermissionName { get; set; } = null!;
}
