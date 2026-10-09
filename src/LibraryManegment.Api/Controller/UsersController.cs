using LibraryManegment.Api.Dtos.User;
using LibraryManegment.Api.Dtos.UserDtos;
using LibraryManegment.Api.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManegment.Api.Constroller;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet("get-users")]
    [Authorize(Policy = "User.Read")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userService.GetAllUsersAsync();

        if (users is null || !users.Any())
        {
            return NotFound("No users found.");
        }

        return Ok(users);
    }

    [HttpPost("create-user")]
    [Authorize(Policy = "User.Create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        var result = await userService.CreateUserAsync(createUserDto);

        if (!result)
        {
            return BadRequest("User with this email already exists.");
        }

        return Ok(new
        {
            message = "User created successfully."
        });
    }

    [HttpGet("get-user-by-id/{id:int}")]
    [Authorize(Policy = "User.Read")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await userService.GetUserByIdAsync(id);

        if (user is null)
        {
            return NotFound("User not found.");
        }

        return Ok(user);
    }

    [HttpGet("get-user-by-email/{email}")]
    [Authorize(Policy = "User.Read")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await userService.GetUserByEmailAsync(email);

        if (user is null)
        {
            return NotFound("User not found.");
        }

        return Ok(user);
    }

    [HttpPut("update-user/{id:int}")]
    [Authorize(Policy = "User.Update")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        var result = await userService.UpdateUserAsync(id, updateUserDto);

        if (!result)
        {
            return NotFound("User not found.");
        }

        return Ok(new
        {
            message = "User updated successfully."
        });
    }

    [HttpPut("update-user-role/{id:int}")]
    [Authorize(Policy = "User.RoleUpdate")]
    public async Task<IActionResult> UpdateUserRole(int id, [FromBody] UpdateUserRoleDto updateUserRoleDto)
    {
        var result = await userService.UpdateUserRoleAsync(
            id,
            updateUserRoleDto);

        if (!result)
        {
            return NotFound("User not found.");
        }

        return Ok(new
        {
            message = "User role updated successfully."
        });
    }

    [HttpDelete("delete-user/{id:int}")]
    [Authorize(Policy = "User.Delete")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await userService.DeleteUserAsync(id);

        if (!result)
        {
            return NotFound("User not found.");
        }

        return Ok(new
        {
            message = "User deleted successfully."
        });
    }
}