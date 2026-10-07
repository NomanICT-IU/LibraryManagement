using LibraryManegment.Api.Dtos.User;
using LibraryManegment.Api.Dtos.UserDtos;

namespace LibraryManegment.Api.Service.Interfaces;

public interface IUserService
{
    Task<bool> CreateUserAsync(CreateUserDto createUserDto);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(int id);
    Task<bool> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
    Task<bool> DeleteUserAsync(int id);
    //Task<bool> ChangePasswordAsync(int id, ChangePasswordDto changePasswordDto);
    //Task<bool> LoginUserAsync(LoginUserDto loginUserDto);
}
