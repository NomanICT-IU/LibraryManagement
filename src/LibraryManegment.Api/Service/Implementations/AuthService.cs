using LibraryManegment.Api.Dtos.UserDtos;
using LibraryManegment.Api.Models;
using LibraryManegment.Api.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LibraryManegment.Api.Service.Implementations;

public class AuthService(IUserService userService, IPasswordHasher<User> passwordHasher, IJwtSigner jwtSigner) : IAuthService
{
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        var user = await userService.GetUserIndentityAsync(loginRequestDto.Email);

        if (user is null)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        var verificationResult =
            passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                loginRequestDto.Password);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        var permissions =
            await userService.GetPermissionsByUserIdAsync(user.Id);

        var (accessToken, expiresOnUtc) =
            jwtSigner.GenerateAccessToken(user, permissions);

        var refreshToken = jwtSigner.GenerateRefreshToken();

        return new LoginResponseDto(
            user.Id,
            user.Email,
            accessToken,
            refreshToken,
            expiresOnUtc);
    }
}