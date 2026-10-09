using LibraryManegment.Api.Dtos.UserDtos;
using LibraryManegment.Api.Models;
using LibraryManegment.Api.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LibraryManegment.Api.Service.Implementations;

public class JwtSigner(IConfiguration configuration) : IJwtSigner
{
    public (string Token, DateTime ExpiresOnUtc) GenerateAccessToken(User user, List<UserPermissionDto> permissions)
    {
        var jwtSettings = configuration.GetSection("Jwt");

        var secretKey = jwtSettings["SecretKey"]
            ?? throw new InvalidOperationException("JWT SecretKey is missing.");

        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        var expiresOnUtc = DateTime.UtcNow.AddMinutes(
            double.Parse(jwtSettings["AccessTokenExpirationMinutes"] ?? "15"));

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email)
        };

        foreach (var permission in permissions)
        {
            claims.Add(new Claim("permission", permission.PermissionName));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiresOnUtc,
            signingCredentials: credentials);

        var tokenString = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return (tokenString, expiresOnUtc);
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);

        return Convert.ToBase64String(randomBytes);
    }
}