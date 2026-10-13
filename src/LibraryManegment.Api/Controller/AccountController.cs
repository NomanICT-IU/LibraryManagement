using LibraryManegment.Api.Dtos.UserDtos;
using LibraryManegment.Api.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManegment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AccountController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        try
        {
            var response = await authService.LoginAsync(loginRequestDto);

            return Ok(response);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new


            {
                Message = "Invalid email or password."
            });
        }
    }
}