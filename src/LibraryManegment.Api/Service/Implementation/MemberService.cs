using LibraryManegment.Api.Data;
using LibraryManegment.Api.Dtos;
using LibraryManegment.Api.Models;
using LibraryManegment.Api.Service.Interface;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LibraryManegment.Api.Service.Implementation;

public class MemberService(LibraryManagementDbContext dbContext) : IMemberService
{
    public async Task<int> CreateAsync(CreateMemberDto member)
    {
        var entity = member.Adapt<Member>();

        entity.CreatedAt = DateTime.UtcNow;

        await dbContext.Members.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
        var member = await dbContext.Members
            .FirstOrDefaultAsync(x => x.Id == Id);

        if (member is null)
            return false;

        dbContext.Members.Remove(member);

        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<MemberDto>> GetAllAsync()
    {
        var members = await dbContext.Members
            .AsNoTracking()
            .ToListAsync();

        return members.Adapt<List<MemberDto>>();
    }

    public async Task<MemberDto> GetByIdAsync(int Id)
    {
        var member = await dbContext.Members
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == Id);

        return member?.Adapt<MemberDto>();
    }

    public async Task<bool> UpdateAsync(UpdateMemberDto member)
    {
        var entity = await dbContext.Members
            .FirstOrDefaultAsync(x => x.Id == member.Id);

        if (entity is null)
            return false;

        member.Adapt(entity);

        entity.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();

        return true;
    }
}