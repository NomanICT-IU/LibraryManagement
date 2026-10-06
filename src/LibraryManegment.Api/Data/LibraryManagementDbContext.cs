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
    public DbSet<Member> Members => Set<Member>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(LibraryManagementDbContext).Assembly);
    }
}
