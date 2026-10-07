
using LibraryManegment.Api.Data;
using LibraryManegment.Api.Dtos.UserInfoDtos;
using LibraryManegment.Api.Models;
using LibraryManegment.Api.Service.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LibraryManegment.Api.Service.Implementations;

public class UserInfoService(LibraryManagementDbContext context) : IUserInfoService
{
    public async Task<bool> CreateUserInfoAsync(CreateUserInfoDto createUserInfoDto)
    {
        var userExists = await context.Users
            .AnyAsync(x => x.Id == createUserInfoDto.UserId);

        if (!userExists)
        {
            return false;
        }

        var roleExists = await context.Roles
            .AnyAsync(x => x.Id == createUserInfoDto.RoleId);

        if (!roleExists)
        {
            return false;
        }

        // Prevent duplicate User + Role
        var alreadyExists = await context.UserInfos
            .AnyAsync(x =>
                x.UserId == createUserInfoDto.UserId &&
                x.RoleId == createUserInfoDto.RoleId);

        if (alreadyExists)
        {
            return false;
        }

        // DTO -> Entity
        var userInfo = createUserInfoDto.Adapt<UserInfo>();

        context.UserInfos.Add(userInfo);

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<UserInfoDto> GetUserInfoByIdAsync(int id)
    {
        var userInfo = await context.UserInfos
            .AsNoTracking()
            .Where(ui => ui.Id == id)
            .Select(ui => new
            {
                Id = ui.Id,
                UserName = ui.User.Name,
                Email = ui.User.Email,
                RoleName = ui.Role.Name,
                Status = ui.Status == 1 ? "Active" : "Inactive"
            })
            .FirstOrDefaultAsync();

        if (userInfo is null)
        {
            return null;
        }

        return userInfo.Adapt<UserInfoDto>();
    }

    public async Task<bool> UpdateUserInfoAsync(int id, UpdateUserInfoDto updateUserInfoDto)
    {
        var userInfo = await context.UserInfos
            .FirstOrDefaultAsync(x => x.Id == id);

        if (userInfo is null)
        {
            return false;
        }

        // Check new Role exists
        var roleExists = await context.Roles
            .AnyAsync(x => x.Id == updateUserInfoDto.RoleId);

        if (!roleExists)
        {
            return false;
        }

        // Check duplicate User + Role
        var duplicateExists = await context.UserInfos
            .AnyAsync(x =>
                x.Id != id &&
                x.UserId == userInfo.UserId &&
                x.RoleId == updateUserInfoDto.RoleId);

        if (duplicateExists)
        {
            return false;
        }

        // DTO -> existing Entity
        updateUserInfoDto.Adapt(userInfo);

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteUserInfoAsync(int id)
    {
        var userInfo = await context.UserInfos
            .FirstOrDefaultAsync(x => x.Id == id);

        if (userInfo is null)
        {
            return false;
        }

        context.UserInfos.Remove(userInfo);

        await context.SaveChangesAsync();

        return true;
    }
}

