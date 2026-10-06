using System.ComponentModel.DataAnnotations;

namespace LibraryManegment.Api.Dtos;

public class CreateMemberDto
{
    [Required(ErrorMessage = "Member Id is required.")]
    [MaxLength(50, ErrorMessage = "Member Id cannot exceed 50 characters.")]
    public string MemberId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [MaxLength(50, ErrorMessage = "Email cannot exceed 50 characters.")]
    public string Email { get; set; } = string.Empty;

    [Range(1, 2, ErrorMessage = "Status must be 1 (Active) or 2 (Inactive).")]
    public int Status { get; set; }
}
