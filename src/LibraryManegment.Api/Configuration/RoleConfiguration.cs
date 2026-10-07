using LibraryManegment.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManegment.Api.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(50);

        builder.HasIndex(x => x.Name)
               .IsUnique();

        builder.HasData(
            new Role
            {
                Id = 1,
                Name = "SuperAdmin"
            },
            new Role
            {
                Id = 2,
                Name = "Admin"
            },
            new Role
            {
                Id = 3,
                Name = "Librarian"
            },
            new Role
            {
                Id = 4,
                Name = "Member"
            }
        );
    }
}