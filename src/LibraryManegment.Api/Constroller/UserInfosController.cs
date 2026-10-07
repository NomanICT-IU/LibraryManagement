
using LibraryManegment.Api.Dtos.UserInfoDtos;
using LibraryManegment.Api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManegment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserInfosController(IUserInfoService userInfoService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUserInfo(
        [FromBody] CreateUserInfoDto createUserInfoDto)
    {
        var result = await userInfoService
            .CreateUserInfoAsync(createUserInfoDto);

        if (!result)
        {
            return BadRequest(
                "User or Role does not exist, or the User already has this Role.");
        }

        return Ok(new
        {
            message = "User role assigned successfully."
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserInfoById(int id)
    {
        var userInfo = await userInfoService
            .GetUserInfoByIdAsync(id);

        if (userInfo is null)
        {
            return NotFound("User information not found.");
        }

        return Ok(userInfo);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUserInfo(
        int id,
        [FromBody] UpdateUserInfoDto updateUserInfoDto)
    {
        var result = await userInfoService
            .UpdateUserInfoAsync(id, updateUserInfoDto);

        if (!result)
        {
            return BadRequest(
                "User information not found, Role does not exist, or the role is already assigned.");
        }

        return Ok(new
        {
            message = "User role updated successfully."
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUserInfo(int id)
    {
        var result = await userInfoService
            .DeleteUserInfoAsync(id);

        if (!result)
        {
            return NotFound("User information not found.");
        }

        return Ok(new
        {
            message = "User role deleted successfully."
        });
    }
}

