using LibraryManegment.Api.Dtos;
using LibraryManegment.Api.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManegment.Api.Constroller;

[Route("api/[controller]")]
[ApiController]
public class MemberController(IMemberService memberService) : ControllerBase
{
    [HttpGet("get-all-member")]
    public async Task<IActionResult> GetAll()
    {
        var members = await memberService.GetAllAsync();

        return Ok(members);
    }


    [HttpGet("(get-member-by-id)/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var member = await memberService.GetByIdAsync(id);

        if (member is null)
            return NotFound(new
            {
                Message = "Member not found."
            });

        return Ok(member);
    }


    [HttpPost("create-member")]
    public async Task<IActionResult> Create([FromBody] CreateMemberDto member)
    {
        var memberId = await memberService.CreateAsync(member);

        return CreatedAtAction(
            nameof(GetById),
            new { id = memberId },
            new
            {
                Id = memberId,
                Message = "Member created successfully."
            });
    }


    [HttpPut("update-member/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMemberDto member)
    {
        member.Id = id;

        var result = await memberService.UpdateAsync(member);

        if (!result)
            return NotFound(new
            {
                Message = "Member not found."
            });

        return Ok(new
        {
            Message = "Member updated successfully."
        });
    }


    [HttpDelete("delete-member/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await memberService.DeleteAsync(id);

        if (!result)
            return NotFound(new
            {
                Message = "Member not found."
            });

        return Ok(new
        {
            Message = "Member deleted successfully."
        });
    }
}