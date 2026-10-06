//using LibraryManegment.Api.Data;
//using LibraryManegment.Api.Dtos;
//using Mapster;
//using Microsoft.EntityFrameworkCore;

//namespace LibraryManegment.Api.Models;

//public interface IMemberRepository
//{
//    Task<IEnumerable<MemberDto>> GetAllAsync(CancellationToken cancellationToken = default);

//    Task<MemberDto> GetByIdAsync(int memberId, CancellationToken cancellationToken = default);

//    Task<bool> CreateAsync(CreateMemberDto member, CancellationToken cancellationToken = default);

//    Task<bool> UpdateAsync(UpdateMemberDto member, CancellationToken cancellationToken = default);

//    Task<bool> DeleteAsync(int Id, CancellationToken cancellationToken = default);
//}

//public class MemberRepository(LibraryManagementDbContext dbContext) : IMemberRepository
//{
//    public async Task<bool> CreateAsync(CreateMemberDto member, CancellationToken cancellationToken = default)
//    {
//        var entity = member.Adapt<Member>();

//        entity.CreatedAt = DateTime.UtcNow;

//        await dbContext.Members.AddAsync(entity, cancellationToken);

//        await dbContext.SaveChangesAsync(cancellationToken);

//        return true;
//    }

//    public async Task<bool> DeleteAsync(int Id, CancellationToken cancellationToken = default)
//    {
//        var member = await dbContext.Members
//            .FirstOrDefaultAsync(
//                x => x.Id == Id,
//                cancellationToken);

//        if (member is null)
//            return false;

//        dbContext.Members.Remove(member);

//        await dbContext.SaveChangesAsync(cancellationToken);

//        return true;
//    }

//    public async Task<IEnumerable<MemberDto>> GetAllAsync(CancellationToken cancellationToken = default)
//    {
//        var members = await dbContext.Members
//            .AsNoTracking()
//            .ToListAsync(cancellationToken);
//        return members.Adapt<IEnumerable<MemberDto>>();
//    }

//    public async Task<MemberDto> GetByIdAsync(int Id, CancellationToken cancellationToken = default)
//    {
//        var member = await dbContext.Members
//            .AsNoTracking()
//            .FirstOrDefaultAsync(
//                x => x.Id == Id,
//                cancellationToken);

//        return member?.Adapt<MemberDto>();
//    }

//    public async Task<bool> UpdateAsync(
//        UpdateMemberDto member,
//        CancellationToken cancellationToken = default)
//    {
//        var entity = await dbContext.Members
//            .FirstOrDefaultAsync(
//                x => x.MemberId == member.MemberId,
//                cancellationToken);

//        if (entity is null)
//            return false;

//        entity.MemberId = member.MemberId;
//        entity.Name = member.Name;
//        entity.Email = member.Email;
//        entity.Status = member.Status;
//        entity.UpdatedAt = DateTime.UtcNow;

//        await dbContext.SaveChangesAsync(cancellationToken);

//        return true;
//    }
//}