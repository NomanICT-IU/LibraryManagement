
using LibraryManegment.Api.Dtos.User;
using LibraryManegment.Api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManegment.Api.Constroller;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet("get-users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userService.GetAllUsersAsync();

        return Ok(users);
    }
    [HttpPost("create-user")]
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
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await userService.GetUserByIdAsync(id);

        if (user is null)
        {
            return NotFound("User not found.");
        }

        return Ok(user);
    }

    [HttpPut("update-user/{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        var result = await userService.UpdateUserAsync(
            id,
            updateUserDto);

        if (!result)
        {
            return NotFound("User not found.");
        }

        return Ok(new
        {
            message = "User updated successfully."
        });
    }

    [HttpDelete("delete-user/{id:int}")]
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

