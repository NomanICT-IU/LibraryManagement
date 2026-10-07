using LibraryManegment.Api.Data;
using LibraryManegment.Api.Dtos.BookDtos;
using LibraryManegment.Api.Models;
using LibraryManegment.Api.Service.Interface;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LibraryManegment.Api.Service.Implementations;

public class BookService(LibraryManagementDbContext context) : IBookService
{
    public async Task<IEnumerable<BookDto>> GetAllAsync()
    {
        var books = await context.Books
            .AsNoTracking()
            .ToListAsync();

        return books.Adapt<IEnumerable<BookDto>>();
    }

    public async Task<BookDto> GetByIdAsync(int id)
    {
        var book = await context.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return book?.Adapt<BookDto>();
    }

    public async Task<BookDto> CreateAsync(CreateBookDto dto)
    {
        var book = dto.Adapt<Book>();

        context.Books.Add(book);

        await context.SaveChangesAsync();

        return book.Adapt<BookDto>();
    }

    public async Task<bool> UpdateAsync(int id, UpdateBookDto dto)
    {
        var book = await context.Books
            .FirstOrDefaultAsync(x => x.Id == id);

        if (book is null)
            return false;

        dto.Adapt(book);

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await context.Books
            .FirstOrDefaultAsync(x => x.Id == id);

        if (book is null)
            return false;

        context.Books.Remove(book);

        await context.SaveChangesAsync();

        return true;
    }
}
