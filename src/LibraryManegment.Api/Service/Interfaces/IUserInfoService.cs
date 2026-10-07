using LibraryManegment.Api.Dtos.UserInfoDtos;

namespace LibraryManegment.Api.Service.Interfaces;

public interface IUserInfoService
{
    Task<bool> CreateUserInfoAsync(CreateUserInfoDto createUserInfoDto);

    Task<UserInfoDto> GetUserInfoByIdAsync(int id);

    Task<bool> UpdateUserInfoAsync(int id, UpdateUserInfoDto updateUserInfoDto);

    Task<bool> DeleteUserInfoAsync(int id);
}

