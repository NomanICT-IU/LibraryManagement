
using LibraryManegment.Api.Data;
using LibraryManegment.Api.Dtos.BookIssue;
using LibraryManegment.Api.Models;
using LibraryManegment.Api.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManegment.Api.Service.Implementations;

public class BookIssueService(LibraryManagementDbContext context) : IBookIssueService
{
    public async Task<bool> IssueBookAsync(CreateBookIssueDto bookIssueDto)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync();

        var book = await context.Books
            .FirstOrDefaultAsync(x => x.Id == bookIssueDto.BookId);

        if (book is null)
        {
            return false;
        }

        if (book.Quantity <= 0)
        {
            return false;
        }

        var userInfoExists = await context.UserInfos
            .AnyAsync(x => x.Id == bookIssueDto.UserInfoId);

        if (!userInfoExists)
        {
            return false;
        }

        var issueDate = DateTime.UtcNow;

        var bookIssue = new BookIssue
        {
            BookId = bookIssueDto.BookId,
            UserInfoId = bookIssueDto.UserInfoId,
            IssueDate = issueDate,
            DueDate = issueDate.AddDays(14),
            ReturnDate = null
        };

        book.Quantity--;

        context.BookIssues.Add(bookIssue);

        await context.SaveChangesAsync();

        await transaction.CommitAsync();

        return true;
    }

    public async Task<bool> ReturnBookIssueAsync(ReturnBookIssueDto bookIssueDto)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync();

        var bookIssue = await context.BookIssues
            .Include(x => x.Book)
            .FirstOrDefaultAsync(
                x => x.Id == bookIssueDto.Id);

        if (bookIssue is null)
        {
            return false;
        }

        if (bookIssue.ReturnDate is not null)
        {
            return false;
        }

        bookIssue.ReturnDate = DateTime.UtcNow;

        bookIssue.Book.Quantity++;

        await context.SaveChangesAsync();

        await transaction.CommitAsync();

        return true;
    }
}

