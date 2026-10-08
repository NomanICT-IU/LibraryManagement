using LibraryManegment.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManegment.Api.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(255);

        builder.HasIndex(x => x.Email)
               .IsUnique();

        builder.Property(x => x.PasswordHash)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
               .IsRequired()
               .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(x => x.UpdatedAt)
               .IsRequired(false);

        // RoleId
        builder.Property(x => x.RoleId)
               .IsRequired()
               .HasDefaultValue(4);

        // Status
        builder.Property(x => x.Status)
               .IsRequired()
               .HasDefaultValue(1);

        // User -> Role relationship
        builder.HasOne(x => x.Role)
               .WithMany()
               .HasForeignKey(x => x.RoleId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}