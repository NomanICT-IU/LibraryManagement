namespace LibraryManegment.Api.Dtos.UserDtos;

public sealed record LoginResponseDto
(
   int UserId,
    string Email,
    string AccessToken,
    string RefreshToken,
    DateTime? AccessTokenExpiresOnUtc);
