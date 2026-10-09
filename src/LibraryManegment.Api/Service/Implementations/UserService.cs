using LibraryManegment.Api.Data;
using LibraryManegment.Api.Dtos.User;
using LibraryManegment.Api.Dtos.UserDtos;
using LibraryManegment.Api.Models;
using LibraryManegment.Api.Service.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManegment.Api.Service.Implementations;

public class UserService : IUserService
{
    private readonly LibraryManagementDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(LibraryManagementDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> CreateUserAsync(CreateUserDto createUserDto)
    {
        // Check if email already exists
        var emailExists = await _context.Users
            .AnyAsync(x => x.Email == createUserDto.Email);

        if (emailExists)
        {
            return false;
        }

        // Map DTO → Entity
        var user = createUserDto.Adapt<User>();

        // Hash password
        user.PasswordHash = _passwordHasher.HashPassword(
            user,
            createUserDto.Password);

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            return false;
        }

        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .ProjectToType<UserDto>()
            .ToListAsync();
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            return null!;
        }

        // Entity → DTO
        return user.Adapt<UserDto>();
    }
    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
        {
            return null;
        }

        // Entity → DTO
        return user.Adapt<UserDto>();
    }
    public async Task<User> GetUserIndentityAsync(string email)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
        {
            return null;
        }
        return user;
    }


    public async Task<bool> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            return false;
        }

        // Map DTO → existing entity
        updateUserDto.Adapt(user);

        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateUserRoleAsync(int id, UpdateUserRoleDto updateUserRoleDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            return false;
        }
        updateUserRoleDto.Adapt(user);

        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<UserPermissionDto>> GetPermissionsByUserIdAsync(int id)
    {
        return await (
            from u in _context.Users.AsNoTracking()
            join rp in _context.RolePermissions.AsNoTracking()
                on u.RoleId equals rp.RoleId
            join r in _context.Roles.AsNoTracking()
                on rp.RoleId equals r.Id
            join p in _context.Permissions.AsNoTracking()
                on rp.PermissionId equals p.Id
            where u.Id == id
            select new UserPermissionDto
            {
                UserId = u.Id,
                RoleName = r.Name,
                PermissionName = p.Name
            }
        ).ToListAsync();
    }
}