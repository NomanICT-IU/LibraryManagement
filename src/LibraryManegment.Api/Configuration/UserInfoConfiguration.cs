
using LibraryManegment.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManegment.Api.Data.Configurations;

public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        // Primary Key
        builder.HasKey(x => x.Id);

        // UserId
        builder.Property(x => x.UserId)
            .IsRequired();

        // RoleId
        builder.Property(x => x.RoleId)
            .IsRequired();

        // Status
        builder.Property(x => x.Status)
            .IsRequired()
            .HasDefaultValue(1);

        // User relationship
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Role relationship
        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Prevent duplicate User + Role assignment
        builder.HasIndex(x => new { x.UserId, x.RoleId })
            .IsUnique();
    }
}

