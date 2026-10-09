using LibraryManegment.Api.Dtos.User;
using LibraryManegment.Api.Dtos.UserDtos;
using LibraryManegment.Api.Models;

namespace LibraryManegment.Api.Service.Interfaces;

public interface IUserService
{
    Task<bool> CreateUserAsync(CreateUserDto createUserDto);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(int id);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<bool> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
    Task<bool> UpdateUserRoleAsync(int id, UpdateUserRoleDto updateUserRoleDto);
    Task<bool> DeleteUserAsync(int id);
    Task<User> GetUserIndentityAsync(string email);
    Task<List<UserPermissionDto>> GetPermissionsByUserIdAsync(int Id);
    //Task<bool> ChangePasswordAsync(int id, ChangePasswordDto changePasswordDto);
    //Task<bool> LoginUserAsync(LoginUserDto loginUserDto);
}
