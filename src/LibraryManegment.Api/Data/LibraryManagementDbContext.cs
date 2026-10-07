using LibraryManegment.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManegment.Api.Data;

public class LibraryManagementDbContext : DbContext
{
    public LibraryManagementDbContext(
           DbContextOptions<LibraryManagementDbContext> options)
           : base(options)
    {
    }
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserInfo> UserInfos => Set<UserInfo>();
    public DbSet<BookIssue> BookIssues => Set<BookIssue>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(LibraryManagementDbContext).Assembly);
    }
}
