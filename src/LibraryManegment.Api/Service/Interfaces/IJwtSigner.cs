using LibraryManegment.Api.Dtos.UserDtos;
using LibraryManegment.Api.Models;

namespace LibraryManegment.Api.Service.Interfaces;

public interface IJwtSigner
{
    (string Token, DateTime ExpiresOnUtc) GenerateAccessToken(User user, List<UserPermissionDto> permissions, List<UserRoleDto> roles);

    string GenerateRefreshToken();
}