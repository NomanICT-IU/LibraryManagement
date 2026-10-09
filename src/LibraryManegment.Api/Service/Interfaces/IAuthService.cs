
using LibraryManegment.Api.Dtos.UserDtos;
namespace LibraryManegment.Api.Service.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
}
