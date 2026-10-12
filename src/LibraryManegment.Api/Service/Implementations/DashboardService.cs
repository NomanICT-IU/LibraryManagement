using LibraryManegment.Api.Data;
using LibraryManegment.Api.Dtos.DashBoardDtos;
using LibraryManegment.Api.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManegment.Api.Service.Implementations;

public class DashboardService(LibraryManagementDbContext context) : IDashboardService
{
    public async Task<DashboardOverviewDto> GetDashboardOverviewAsync()
    {

        var totalBook = await context.Books.CountAsync();


        var borrowedCopies = await context.IssueBooks.CountAsync(ib => ib.ReturnDate == null);

        var totalBookCopies = await context.Books.SumAsync(b => b.Quantity);
        var availableCopies = totalBookCopies - borrowedCopies;


        var allMembers = await context.Users.CountAsync(u => u.RoleId == 4);


        var recentBorrowedBooks = await context.IssueBooks
            .Where(ib => ib.ReturnDate == null)
            .OrderByDescending(ib => ib.IssueDate)
            .Take(5)
            .Select(ib => new RecentBorrowedBookDto
            {
                BookTitle = ib.Book.Name,
                Member = ib.User.Name,
                IssuedDate = ib.IssueDate.ToString("yyyy-MM-dd"),
                DueDate = ib.DueDate.ToString("yyyy-MM-dd")
            })
            .ToListAsync();

        return new DashboardOverviewDto
        {
            TotalBook = totalBook,
            AvailableCopies = availableCopies,
            BorrowedCopies = borrowedCopies,
            AllMembers = allMembers,
            RecentBorrowedBooks = recentBorrowedBooks
        };
    }
}