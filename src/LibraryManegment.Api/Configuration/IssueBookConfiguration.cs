
using LibraryManegment.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManegment.Api.Data.Configurations;

public class IssueBookConfiguration : IEntityTypeConfiguration<IssueBook>
{
    public void Configure(EntityTypeBuilder<IssueBook> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BookId)
            .IsRequired();

        builder.Property(x => x.UserId)
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

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.BookId, x.UserId });
    }
}

