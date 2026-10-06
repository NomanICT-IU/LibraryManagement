using LibraryManegment.Api.Dtos;

namespace LibraryManegment.Api.Service.Interface;

public interface IMemberService
{
    Task<IEnumerable<MemberDto>> GetAllAsync();

    Task<MemberDto> GetByIdAsync(int memberId);

    Task<int> CreateAsync(CreateMemberDto member);

    Task<bool> UpdateAsync(UpdateMemberDto member);

    Task<bool> DeleteAsync(int memberId);
}