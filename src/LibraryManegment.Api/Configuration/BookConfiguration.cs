using LibraryManegment.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManegment.Api.Configuration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(x => x.Author)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(x => x.ISBN)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(x => x.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(x => x.Quantity)
               .IsRequired();

        builder.HasIndex(x => x.ISBN)
               .IsUnique();
    }
}