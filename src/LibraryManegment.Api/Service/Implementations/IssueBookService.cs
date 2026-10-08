using LibraryManegment.Api.Data;
using LibraryManegment.Api.Dtos.BookIssue;
using LibraryManegment.Api.Dtos.IssueBookDtos;
using LibraryManegment.Api.Models;
using LibraryManegment.Api.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManegment.Api.Service.Implementations;

public class IssueBookService(LibraryManagementDbContext context) : IIssueBookService
{
    public async Task<bool> IssueBookAsync(CreateIssueBookDto issueBookDto)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync();

        var book = await context.Books
            .FirstOrDefaultAsync(x => x.Id == issueBookDto.BookId);

        if (book is null)
        {
            return false;
        }

        if (book.Quantity <= 0)
        {
            return false;
        }

        var userExists = await context.Users
            .AnyAsync(x => x.Id == issueBookDto.UserId);

        if (!userExists)
        {
            return false;
        }

        var issueDate = DateTime.UtcNow;

        var bookIssue = new IssueBook
        {
            BookId = issueBookDto.BookId,
            UserId = issueBookDto.UserId,
            IssueDate = issueDate,
            DueDate = issueDate.AddDays(14),
            ReturnDate = null
        };

        book.Quantity--;

        context.IssueBooks.Add(bookIssue);

        await context.SaveChangesAsync();
        await transaction.CommitAsync();

        return true;
    }

    public async Task<bool> ReturnIssueBookAsync(ReturnIssueBookDto issueBookDto)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync();

        var bookIssue = await context.IssueBooks
            .Include(x => x.Book)
            .FirstOrDefaultAsync(x => x.Id == issueBookDto.Id);

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

    public async Task<List<IssueBookDto>> GetIssueBooksAsync()
    {
        return await context.IssueBooks
            .AsNoTracking()
            .Where(x => x.ReturnDate == null)
            .Select(x => new IssueBookDto
            {
                Id = x.Id,
                UserName = x.User.Name,
                BookName = x.Book.Name,
                IssueDate = x.IssueDate,
                DueDate = x.DueDate
            })
            .ToListAsync();
    }
    public async Task<IssueBookDto> GetIssueBookByIdAsync(int id)
    {
        return await context.IssueBooks
            .AsNoTracking()
            .Where(x => x.Id == id && x.ReturnDate == null)
            .Select(x => new IssueBookDto
            {
                Id = x.Id,
                UserName = x.User.Name,
                BookName = x.Book.Name,
                IssueDate = x.IssueDate,
                DueDate = x.DueDate
            })
            .FirstOrDefaultAsync();
    }
    public async Task<List<IssueBookDto>> GetIssueBooksByEmailAsync(string email)
    {
        return await context.IssueBooks
            .AsNoTracking()
            .Where(x =>
                x.User.Email == email &&
                x.ReturnDate == null)
            .Select(x => new IssueBookDto
            {
                Id = x.Id,
                UserName = x.User.Name,
                BookName = x.Book.Name,
                IssueDate = x.IssueDate,
                DueDate = x.DueDate
            })
            .ToListAsync();
    }
    public async Task<bool> RequestDueDateExtensionAsync(ExtentedRequestDto requestDto)
    {
        var issueBook = await context.IssueBooks
            .FirstOrDefaultAsync(x => x.Id == requestDto.Id);

        if (issueBook is null)
        {
            return false;
        }

        if (issueBook.ReturnDate is not null)
        {
            return false;
        }

        issueBook.RequestedDueDate = requestDto.RequestedDueDate;
        issueBook.RoleId = requestDto.RoleId;
        issueBook.Status = 1;

        await context.SaveChangesAsync();

        return true;
    }
    public async Task<List<GetExtensionRequestDto>> GetExtensionRequestsAsync(int roleId)
    {
        return await context.IssueBooks
            .AsNoTracking()
            .Where(x =>
                x.ReturnDate == null &&
                x.Status == 1 &&
                x.RoleId == roleId)
            .Select(x => new GetExtensionRequestDto
            {
                Id = x.Id,
                UserName = x.User.Name,
                BookName = x.Book.Name,
                DueDate = x.DueDate,
                RequestedDueDate = x.RequestedDueDate
            })
            .ToListAsync();
    }
    public async Task<bool> ApproveExtensionRequestAsync(int id)
    {
        var issueBook = await context.IssueBooks
            .FirstOrDefaultAsync(x => x.Id == id);

        if (issueBook is null)
            return false;

        if (issueBook.ReturnDate is not null)
            return false;

        if (issueBook.Status != 1)
            return false;

        if (issueBook.RequestedDueDate is null)
            return false;

        issueBook.DueDate = issueBook.RequestedDueDate.Value;
        issueBook.Status = 2;

        await context.SaveChangesAsync();

        return true;
    }
    public async Task<bool> RejectExtensionRequestAsync(int id)
    {
        var issueBook = await context.IssueBooks
            .FirstOrDefaultAsync(x => x.Id == id);

        if (issueBook is null)
            return false;

        if (issueBook.ReturnDate is not null)
            return false;

        if (issueBook.Status != 1)
            return false;

        issueBook.Status = 3;

        await context.SaveChangesAsync();

        return true;
    }
}