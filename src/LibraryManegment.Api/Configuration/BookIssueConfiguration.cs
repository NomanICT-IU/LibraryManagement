
using LibraryManegment.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManegment.Api.Data.Configurations;

public class BookIssueConfiguration : IEntityTypeConfiguration<BookIssue>
{
    public void Configure(EntityTypeBuilder<BookIssue> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BookId)
            .IsRequired();

        builder.Property(x => x.UserInfoId)
            .IsRequired();

        builder.Property(x => x.IssueDate)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(x => x.DueDate)
            .IsRequired();

        builder.Property(x => x.ReturnDate)
            .IsRequired(false);

        builder.HasOne(x => x.Book)
            .WithMany()
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.UserInfo)
            .WithMany()
            .HasForeignKey(x => x.UserInfoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.BookId, x.UserInfoId });
    }
}

